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
        //������ ���� �� �̺�Ʈ
        OnItemPickupItem?.Invoke();
        
        //������ ��Ȱ��ȭ
        gameObject.SetActive(false);
    }
}
