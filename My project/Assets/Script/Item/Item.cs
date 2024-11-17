using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    public event Action OnItemPickupItem;
    public ItemData itemData;


    private void Start()
    {
        AddOnItemPickupItem();
    }
    void AddOnItemPickupItem()
    {
        OnItemPickupItem += ItemEnqueue;
    }
    void ItemEnqueue()
    {
        ObjectPoolingManager.Instance.itemPool[gameObject.name].Enqueue(gameObject);
    }

    public void PickupItem()
    {
        //아이템 수집 시 이벤트
        OnItemPickupItem?.Invoke();
        
        //아이템 비활성화
        gameObject.SetActive(false);
    }
}
