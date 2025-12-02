using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class PickupUi : MonoBehaviour
{
    private Image _image;

    void Start()
    {
        _image = GetComponent<Image>();
    }

    public void ShowItem(Sprite image)
    {
        _image.sprite = image;
        _image.SetNativeSize();
        _image.enabled = true;
    }

    public void HideItem()
    {
        _image.enabled = false;
    }
}
