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
        gameObject.SetActive(true);
    }

    public void HideItem()
    {
        gameObject.SetActive(false);
    }
}
