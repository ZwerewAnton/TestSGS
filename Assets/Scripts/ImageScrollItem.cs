using UnityEngine;
using UnityEngine.UI;

public class ImageScrollItem : MonoBehaviour
{
    public Button button;
    public Image image;
    public ImageViewer imageViewer;
    private void Start()
    {
        imageViewer = FindObjectOfType<ImageViewer>();
        button.GetComponent<Button>().onClick.AddListener(delegate { imageViewer.ShowViewer(image.sprite); });
    }
}
