using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragSlot : MonoBehaviour
{
    [HideInInspector]public Slot slot;//���������
    [HideInInspector]public Image image;//�̹��� ���۳�Ʈ �����
   
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
