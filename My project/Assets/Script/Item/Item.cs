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
        ItemManager.Instance.itemPool[gameObject.name].Enqueue(gameObject);
    }

    public ItemData PickupItem()
    {
        //������ ���� �� �̺�Ʈ
        OnItemPickupItem?.Invoke();
        
        //������ ��Ȱ��ȭ
        gameObject.SetActive(false);
        return itemData;
    }
}
