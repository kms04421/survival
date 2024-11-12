using System;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [HideInInspector]public Slot[] slots;
    public GameObject invnetoryObj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slots = new Slot[33];
        int invnetoryCount = invnetoryObj.transform.childCount;
        for (int i = 0; i < invnetoryCount; i++)
        {
            Transform invnetoryObjtransform = invnetoryObj.transform.GetChild(i);
            slots[i] = invnetoryObjtransform.GetComponent<Slot>();
        }

    }

    public void AddItem(ItemData itemData)
    {
        bool itemAdded = false;
        for (int i = 0; i < slots.Length; i++)
        {
            // ���� �������� �̹� �ִٸ�
            if (slots[i].itemData != null && slots[i].itemData.name == itemData.name)
            {
                slots[i].quantity++;
                slots[i].textMeshPro.text = "x"+ slots[i].quantity.ToString();
                itemAdded = true;
                break;
            }

            // �� ������ �ִٸ� �ش� ���Կ� ������ �߰�
            if (slots[i].itemData == null && !itemAdded)
            {
                slots[i].itemData = itemData;
                slots[i].image.sprite = itemData.itemPrefab.GetComponent<SpriteRenderer>().sprite;
                slots[i].quantity = 1;  // �������� �߰��� �� �ʱ� ������ 1�� ����
                itemAdded = true;
                break;
            }
        }
    }
}
