using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    public event Action<Item> OnItemPickupItem;
    public ItemData itemData;
   
    public ItemData PickupItem()
    {
        //������ ���� �� �̺�Ʈ
        OnItemPickupItem?.Invoke(this);

        //������ ��Ȱ��ȭ
        gameObject.SetActive(false);
        return itemData;
    }
}
