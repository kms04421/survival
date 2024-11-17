using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragSlot : MonoBehaviour
{
    [HideInInspector]public Slot slot;//슬롯저장용
    [HideInInspector]public Image image;//이미지 컴퍼넌트 저장용
   
    private void OnEnable()
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
