using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
public class Inventory : SingletonBehaviour<Inventory>
{
    [HideInInspector] public Slot[] slots;
    [HideInInspector] public Slot[] mixture;

    private Dictionary<string, int> itemCounts;
    public GameObject invnetoryObj;
    public GameObject mixtureObj;
    public List<ItemData> mixitemList;//���� ������ ����Ʈ 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        itemCounts = new Dictionary<string, int>();
        SetArray(ref slots, invnetoryObj, 33); // �κ��丮 �迭
        SetArray(ref mixture, mixtureObj, 5); // ���չ迭

    }

    public void SetArray(ref Slot[] array, GameObject tragetObj, int size)//Ÿ�ٿ�����Ʈ �迭�� ����
    {
        array = new Slot[size];
        int invnetoryCount = tragetObj.transform.childCount;
        for (int i = 0; i < invnetoryCount; i++)
        {
            Transform invnetoryObjtransform = tragetObj.transform.GetChild(i);
            array[i] = invnetoryObjtransform.GetComponent<Slot>();
        }
    }

    public void AddItem(ItemData itemData)// ������ �߰�
    {

        bool itemAdded = false;
        int index = -1;
        for (int i = 0; i < slots.Length; i++)
        {
            // ���� �������� �̹� �ִٸ�
            if (slots[i].itemData != null && slots[i].itemData.name == itemData.name)
            {

                slots[i].quantity++;
                slots[i].textMeshPro.text = "x" + slots[i].quantity.ToString();
                itemAdded = false;
                break;
            }

            // �� ������ �ִٸ� �ش� ���Կ� ������ �߰�
            if (slots[i].itemData == null && !itemAdded)
            {
                index = i;
                itemAdded = true;
            }
        }
        if (itemAdded)
        {
            itemData.icon = itemData.itemPrefab.GetComponent<SpriteRenderer>().sprite;
            slots[index].itemData = itemData;
            slots[index].image.sprite = itemData.icon;
            slots[index].quantity = 1;  // �������� �߰��� �� �ʱ� ������ 1�� ����
            slots[index].textMeshPro.text = "x" + slots[index].quantity.ToString();
        }
    }

    public int ItemSerch(string code) // �κ��丮���� ������ Ž��
    {
        for (int i = 0; i < slots.Length; i++)
        {
           
            if (slots[i].itemData!= null && slots[i].itemData.itemCode.Equals(code))
            {
                return i;
            }
        }
        return -1;
    }

    public bool MinItem() //���ս� ������ ����
    {
        bool isbool = true;
        itemCounts.Clear(); // ���� ������ �ʱ�ȭ

        for (int i = 0; i < mixture.Length-1; i++)  // �� �������� ������ Dictionary�� �߰�
        {
            if (mixture[i].itemData != null)
            {
                if (itemCounts.ContainsKey(mixture[i].itemData.itemCode))
                {
                    itemCounts[mixture[i].itemData.itemCode]++;

                }
                else
                {
                    itemCounts.Add(mixture[i].itemData.itemCode, 1);
                }
               
                if (slots[ItemSerch(mixture[i].itemData.itemCode)].quantity < itemCounts[mixture[i].itemData.itemCode]) //���ռ��� ���� ������ üũ
                {
                    
                    isbool = false;
                }
            }
        }
        if (!isbool) return false;

        foreach (var dic in itemCounts)
        {      
            int index = ItemSerch(dic.Key);
            if (index != -1)
            {
                int min = slots[index].quantity - dic.Value;
                slots[index].quantity = min;
                if (slots[index].quantity == 0)
                {
                    slots[index].Init();
                }
                else
                {
                    slots[index].textMeshPro.text ="x"+ min.ToString();
                }
            }
        }
        for (int i = 0; i < mixture.Length-1; i++)
        {
            mixture[i].Init();
        }  
          
        

        return isbool;

    }

    public string MixCodeSum() // ������ �ڵ� ����
    {
        StringBuilder sum = new StringBuilder();

        for (int i = 0; i < mixture.Length - 1; i++)
        {
            if (mixture[i].itemData?.itemCode == null)
            {
                sum.Append("0");
            }
            else
            {
                sum.Append(mixture[i].itemData.itemCode);
            }
        }

        return sum.ToString();
    }
    public void CraftItem() // ���� ������ �˻��� ��ȯ
    {
        string mixcode = MixCodeSum();
        for (int i = 0; i < mixitemList.Count; i++)
        {
            if (mixitemList[i].itemCode.Equals(mixcode))
            {
                mixture[4].itemData = mixitemList[i];
                mixture[4].itemData.icon = mixitemList[i].itemPrefab.GetComponent<SpriteRenderer>().sprite;
                mixture[4].image.sprite = mixture[4].itemData.icon;
                mixture[4].quantity = 1;
            }
        }

    }
   


}
