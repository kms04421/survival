using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragSlot : MonoBehaviour
{
    public Slot slot;
    public Image image;
    public ItemData itemData;

    private void Start()
    {
      image = GetComponent<Image>();   
    }

    public void DragSetImage(Image _image)
    {
        image.sprite = _image.sprite;
        SetColor(1);
    }

    public void SetColor(float _alpha)
    {
        Color color = image.color;
        color.a = _alpha;
        image.color = color;
    }
}
