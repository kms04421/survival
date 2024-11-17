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

                SwapItems(droppedSlot);// 아이템 스왑
                DropEvent();

                break;
            case SlotType.Mixture:
                MixtureItemSlotDrop(droppedSlot); // 조합칸에 복제 
                Inventory.Instance.CraftItem();
                break;
            case SlotType.Inventory:

                if (!ProcessInventorySlotDrop(droppedSlot)) return; // 인벤토리에 드랍된 슬롯 타입 에따라 드롭한 슬롯 이벤트 실행 
                SwapItems(droppedSlot);// 아이템 스왑

                break;
            case SlotType.result:

                break;
        }
 

    }

    public bool ProcessInventorySlotDrop(Slot droppedSlot)// 인벤토리에 드랍된 슬롯 타입 에따라 드롭한 슬롯 이벤트 실행 
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
    public void DropEvent() // 드롭 이벤트 실행
    {
        OnDropEvent?.Invoke();
    }
    public void SwapEvent() // 스왑시 이벤트 실행 
    {
        OnSwapEvent?.Invoke();
    }


    private void SwapItems(Slot otherSlot)//슬롯정보 스왑
    {

        ItemData tempItemData = itemData;
        int tempQuantity = quantity;

        itemData = otherSlot.itemData;
        quantity = otherSlot.quantity;

        otherSlot.itemData = tempItemData;
        otherSlot.quantity = tempQuantity;

        // 이미지와 텍스트 업데이트
        UpdateSlotUI();
        otherSlot.UpdateSlotUI();

    }

    private void UpdateSlotUI() //ui스왑
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

    private void MixtureItemSlotDrop(Slot slot) //조합슬롯에 드롭시 아이템 수량 증가 혹은 새로 추가
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

    public void Init() // slot초기화
    {
        image.sprite = null;
        quantityText.text = "";
        quantity = 0;
        itemData = null;
    }
    public void SetSlot(ItemData _itemData) // 슬로 기본정보 세팅
    {
        if (_itemData == null) return;
        image.sprite = _itemData.icon;
        itemData = _itemData;
        quantity = 0;
        AddQuantity();
    }

    public void AddQuantity()//quantity 수량추가
    {
        quantity++;
        quantityText.text = "x" + quantity.ToString();
    }
    public void DecCountQuantity(int num) // quantity 수량 차감 0이면 초기화
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
