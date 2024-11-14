using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Slot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public Image image;

    public TextMeshProUGUI textMeshPro;
    public int quantity = 0;
    public ItemData itemData;
    [SerializeField] private SlotType slotType;

    public UnityEvent onActionTriggered;
    private enum SlotType
    {
        Equipment,
        Mixture,
        result,
        Inventory
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        if (itemData == null) { return; }
        UIManager.Instance.dragSlot.slot = this;
        UIManager.Instance.dragSlot.DragSetImage(image);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (itemData != null)
        {
            UIManager.Instance.dragSlot.transform.position = eventData.position;
        }

    }
    public void OnDrop(PointerEventData eventData)
    {
        Slot droppedSlot = eventData.pointerDrag.GetComponent<Slot>();
        if (droppedSlot == null) return;
        switch (slotType)
        {
            case SlotType.Equipment:
                if (droppedSlot.itemData.itemType != ItemType.Weapon) return;

                SwapItems(droppedSlot);// ������ ����

                break;
            case SlotType.Mixture:
                MixtureItemSlotDrop(droppedSlot); // ����ĭ�� ���� 
                Inventory.Instance.CraftItem();
                break;
            case SlotType.Inventory:
                if (droppedSlot.slotType == SlotType.result)
                {
                    if (itemData != null) return;
                    if (!Inventory.Instance.MinItem()) return;
                }
                if (droppedSlot.slotType == SlotType.Mixture)
                {
                    droppedSlot.Init();
                    return;
                }
                SwapItems(droppedSlot);// ������ ����

                break;
            case SlotType.result:

                break;
        }



    }

    public void OnEvent()
    {
        onActionTriggered?.Invoke();
    }
    public void OnEndDrag(PointerEventData eventData)
    {

        UIManager.Instance.dragSlot.SetColor(0);
        UIManager.Instance.dragSlot.slot = null;

    }


    private void SwapItems(Slot otherSlot)//�������� ����
    {

        ItemData tempItemData = itemData;
        int tempQuantity = quantity;

        itemData = otherSlot.itemData;
        quantity = otherSlot.quantity;

        otherSlot.itemData = tempItemData;
        otherSlot.quantity = tempQuantity;

        // �̹����� �ؽ�Ʈ ������Ʈ
        UpdateSlotUI();
        otherSlot.UpdateSlotUI();
        OnEvent();
        otherSlot.OnEvent();
    }

    private void UpdateSlotUI() //ui����
    {
        if (itemData != null)
        {
            image.sprite = itemData.icon;
            textMeshPro.text = "x" + quantity.ToString();

        }
        else
        {
            image.sprite = null;
            textMeshPro.text = "";

        }
    }

    private void MixtureItemSlotDrop(Slot slot) //���ս��Կ� ��ӽ� ������ ���� ���� Ȥ�� ���� �߰�
    {
        if (itemData == null || !itemData.itemCode.Equals(slot.itemData.itemCode))
        {
            quantity = 1;
            textMeshPro.text = "x" + quantity.ToString();
        }
        else if (slot.quantity > quantity)
        {
            quantity++;
            textMeshPro.text = "x" + quantity.ToString();
        }

        itemData = slot.itemData;
        image.sprite = slot.itemData.icon;
    }

    public void Init()
    {
        image.sprite = null;
        textMeshPro.text = "";
        quantity = 0;
        itemData = null;
    }
    private void OnDisable()
    {
        if (slotType == SlotType.Mixture || slotType == SlotType.result)
        {
            Init();
        }
    }

    public void LinkSlot(Slot slot)
    {
        itemData = slot.itemData;
        image.sprite = slot.image.sprite;
        if (slot.quantity > 0 && slot.itemData.itemType != ItemType.Weapon)
        {
            textMeshPro.text = "x" + slot.quantity.ToString();

        }
        else
        {
            textMeshPro.text = "";
        }
    }
}
