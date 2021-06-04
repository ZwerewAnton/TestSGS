using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drawer : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject brush;
    public List<Material> materialList = new List<Material>();

    private LineRenderer _currentLineRenderer;
    private Vector3 _lastPos;
    private Material _currentMaterial;
    private List<GameObject> _brushList = new List<GameObject>();

    private void Start()
    {
        _currentMaterial = materialList[0];
    }
    private void Update()
    {
        Drawing();
    }

    private void Drawing() 
    {   
        if(!DetectPointerOverGameobject())
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                CreateBrush();
            }
            else if (Input.GetKey(KeyCode.Mouse0) && _currentLineRenderer != null)
            {
                PointToMousePos();
            }
            else 
            {
                _currentLineRenderer = null;
            }
        }

    }

    private void CreateBrush() 
    {
        GameObject brushInstance = Instantiate(brush);
        _brushList.Add(brushInstance);
        _currentLineRenderer = brushInstance.GetComponent<LineRenderer>();
        _currentLineRenderer.material = _currentMaterial;
        Vector3 mousePos = GetMousePosition();

        _currentLineRenderer.SetPosition(0, mousePos);
        _currentLineRenderer.SetPosition(1, mousePos);

    }
    private void PointToMousePos() 
    {
        Vector3 mousePos = GetMousePosition();
        if (_lastPos != mousePos) 
        {
            AddAPoint(mousePos);
            _lastPos = mousePos;
        }
    }

    private void AddAPoint(Vector2 pointPos) 
    {
        _currentLineRenderer.positionCount++;
        int positionIndex = _currentLineRenderer.positionCount - 1;
        _currentLineRenderer.SetPosition(positionIndex, pointPos);
    }

    private Vector3 GetMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f;
        mousePosition = mainCamera.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0.0f;
        return mousePosition;
    }

    public void ChangeColor(int index)
    {
        if(materialList.Count >= index)
        {
            _currentMaterial = materialList[index];
        }
    }

    public void Clear()
    {
        foreach(var brush in _brushList)
        {
            Destroy(brush);
        }
    }
    private bool DetectPointerOverGameobject()
    {
        #if UNITY_EDITOR
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }
        #endif
        if(Input.touchSupported)
        {
            foreach(var touch in Input.touches)
            {
                if(EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    return true;
                }
            }
        }
        return false;
    }
}
