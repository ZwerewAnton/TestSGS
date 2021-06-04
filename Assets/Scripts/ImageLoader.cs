using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ImageLoader : MonoBehaviour
{
    [Range(0.1f, 1f)] [SerializeField] private float loadingThreshold = 0.1f;
    [SerializeField] private  ScrollRect _scrollRect;
    [SerializeField] private string _imageUrlPath;
    [SerializeField] private RectTransform _contentRectTranform;
    [SerializeField] private GameObject _imagePrefab;
    [SerializeField] private GridLayoutGroup _contentGridLayoutGroup;
    [SerializeField] private int _imagesCount = 1;
    private int _columnCount;
    private int _currentImageIndex;
    private bool _isImagesLoaded = true;

    private void Start()
    {
        _columnCount = _contentGridLayoutGroup.constraintCount;
        _currentImageIndex = 1;
    }
    private void Update()
    {
        if(_isImagesLoaded && _scrollRect.verticalNormalizedPosition < loadingThreshold && _currentImageIndex <= _imagesCount)
        {
            StartCoroutine(LoadImages());
            _isImagesLoaded = false;
        }
    }

    private void OnEnable()
    {
        _isImagesLoaded = true;
    }

    public IEnumerator LoadImages()
    {
        for(int i = 1; i <= _columnCount; i++)
        {
            string path = _imageUrlPath + _currentImageIndex.ToString() + ".jpg";
            yield return LoadTextureFromServer(path, CreateImageObject);
        }
        
        Canvas.ForceUpdateCanvases();
        _isImagesLoaded = true;
    }

    private void CreateImageObject(Texture2D texture)
    {
        if(texture != null)
        {
            GameObject imageObject = Instantiate(_imagePrefab, _contentRectTranform);
            imageObject.GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            //imageObject.GetComponents<Button>().
            _currentImageIndex++;
        }
    }

    IEnumerator LoadTextureFromServer(string url, Action<Texture2D> result)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);

        yield return request.SendWebRequest();

        Texture2D loadedTexture = new Texture2D(1, 1);
        if (string.IsNullOrEmpty(request.error))
        {
            result(DownloadHandlerTexture.GetContent(request));
        }
        else
        {
            Debug.LogErrorFormat("error request [{0}, {1}]", url, request.error);
            result(null);
        }
        request.Dispose();
    }
}

