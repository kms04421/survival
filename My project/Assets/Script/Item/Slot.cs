using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Slot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private Image image;
    private TextMeshProUGUI quantityText;
    [HideInInspector] public int quantity = 0;
    public ItemData itemData;
    [SerializeField] private SlotType slotType;
    public UnityEvent OnSwapEvent;
    public UnityEvent OnDropEvent;

    public void Start()
    {
        SetSlot(itemData);
    }
    private void OnEnable()
    {
        image = transform.GetChild(0).GetComponent<Image>();
        quantityText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        
    }

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
                DropEvent();

                break;
            case SlotType.Mixture:
                MixtureItemSlotDrop(droppedSlot); // ����ĭ�� ���� 
                Inventory.Instance.CraftItem();
                break;
            case SlotType.Inventory:

                if (!ProcessInventorySlotDrop(droppedSlot)) return; // �κ��丮�� ����� ���� Ÿ�� ������ ����� ���� �̺�Ʈ ���� 
                SwapItems(droppedSlot);// ������ ����

                break;
            case SlotType.result:

                break;
        }
 

    }

    public bool ProcessInventorySlotDrop(Slot droppedSlot)// �κ��丮�� ����� ���� Ÿ�� ������ ����� ���� �̺�Ʈ ���� 
    {
        switch (droppedSlot.slotType)
        {
            case SlotType.Equipment:
                droppedSlot.DropEvent();
                break;
            case SlotType.Mixture:
                droppedSlot.Init();
                Inventory.Instance.CraftItem();
                return false;
              
            case SlotType.Inventory:
               
                break;
            case SlotType.result:
                if (itemData != null) return false;
                if (!Inventory.Instance.MinItem()) return false;
                break;

        }
        return true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        UIManager.Instance.dragSlot.SetColor(0);
        UIManager.Instance.dragSlot.slot = null;

    }
    public void DropEvent() // ��� �̺�Ʈ ����
    {
        OnDropEvent?.Invoke();
    }
    public void SwapEvent() // ���ҽ� �̺�Ʈ ���� 
    {
        OnSwapEvent?.Invoke();
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

    }

    private void UpdateSlotUI() //ui����
    {
        if (itemData != null)
        {
            image.sprite = itemData.icon;
            quantityText.text = "x" + quantity.ToString();

        }
        else
        {
            image.sprite = null;
            quantityText.text = "";

        }
    }

    private void MixtureItemSlotDrop(Slot slot) //���ս��Կ� ��ӽ� ������ ���� ���� Ȥ�� ���� �߰�
    {
       
        if (itemData == null || !itemData.itemCode.Equals(slot.itemData.itemCode))
        {
            quantity = 1;
            quantityText.text = "x" + quantity.ToString();
        }
        else if (slot.quantity > quantity)
        {
            quantity++;
            quantityText.text = "x" + quantity.ToString();
        }

        itemData = slot.itemData;
        image.sprite = slot.itemData.icon;
    }

    public void Init() // slot�ʱ�ȭ
    {
        image.sprite = null;
        quantityText.text = "";
        quantity = 0;
        itemData = null;
    }
    public void SetSlot(ItemData _itemData) // ���� �⺻���� ����
    {
        if (_itemData == null) return;
        image.sprite = _itemData.icon;
        itemData = _itemData;
        quantity = 0;
        AddQuantity();
    }

    public void AddQuantity()//quantity �����߰�
    {
        quantity++;
        quantityText.text = "x" + quantity.ToString();
    }
    public void DecCountQuantity(int num) // quantity ���� ���� 0�̸� �ʱ�ȭ
    {
       
        int tempquantity = quantity - num;
        if (tempquantity <= 0)
        {
            Init();
            return;
        }
        quantity = tempquantity;
        quantityText.text = "x" + quantity.ToString();
    }
    public void OnDisable()
    {
        if(slotType == SlotType.Mixture || slotType == SlotType.result)
        {
            Init();
        }
    }
}
