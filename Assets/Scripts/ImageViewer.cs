using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ImageViewer : MonoBehaviour
{
    public GameObject mainCanvas;
    public GameObject imageCanvas;
    public Image image;
    private bool _isImageViewActive;
	[SerializeField] private UnityEvent _onPressBackButtonEvent;

    private void Start()
    {
        ShowScrollView();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(_isImageViewActive)
            {
                ShowScrollView();
            }
            else
            {
                SetAnyOrientation();
                _onPressBackButtonEvent.Invoke();
            }
        }
    }
    public void ShowViewer(Sprite sprite)
    {
        mainCanvas.SetActive(false);
        imageCanvas.SetActive(true);
        image.sprite = sprite;
        _isImageViewActive = true;
        SetAnyOrientation();
    }
    public void ShowScrollView()
    {
        mainCanvas.SetActive(true);
        imageCanvas.SetActive(false);
        _isImageViewActive = false;
        SetPortraitOrientation();
    }

    private void SetPortraitOrientation()
    {
        Screen.orientation = UnityEngine.ScreenOrientation.Portrait;
        Screen.autorotateToPortrait = Screen.autorotateToPortraitUpsideDown = true;
        Screen.autorotateToLandscapeLeft = Screen.autorotateToLandscapeRight = false;
    }
    
    public void SetAnyOrientation()
    {
        Screen.orientation = UnityEngine.ScreenOrientation.AutoRotation;
        Screen.autorotateToPortrait = Screen.autorotateToPortraitUpsideDown = true;
        Screen.autorotateToLandscapeLeft = Screen.autorotateToLandscapeRight = true;
    }
}
