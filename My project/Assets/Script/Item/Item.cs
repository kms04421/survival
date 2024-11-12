using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    public event Action<Item> OnItemPickupItem;
    public ItemData itemData;
   
    public ItemData PickupItem()
    {
        //아이템 수집 시 이벤트
        OnItemPickupItem?.Invoke(this);

        //아이템 비활성화
        gameObject.SetActive(false);
        return itemData;
    }
}
