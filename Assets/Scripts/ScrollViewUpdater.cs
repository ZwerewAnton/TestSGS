using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewUpdater : MonoBehaviour
{
    public ScrollRect scrollRect;
    private float _previousVerticalPosition = 1f;
    [SerializeField] private RectTransform _contentRectTranform;
    [SerializeField] private GameObject _imagePrefab;
    [SerializeField] private ImageLoader _imageLoader;

    void Update()
    {
        if(scrollRect.verticalNormalizedPosition < 0.1)
        {
            Instantiate(_imagePrefab, _contentRectTranform);
            Instantiate(_imagePrefab, _contentRectTranform);
        }

        Debug.Log(scrollRect.verticalNormalizedPosition);
        if(isLoading())
        Debug.Log(scrollRect.verticalNormalizedPosition);
    }

    bool isLoading(){
        bool isLoad = false;
        if(scrollRect.verticalNormalizedPosition < (_previousVerticalPosition - 0.8))
        {
            isLoad = true;
            _previousVerticalPosition = scrollRect.verticalNormalizedPosition;
            Debug.Log("Load");
        }


        return isLoad;
    }
}
