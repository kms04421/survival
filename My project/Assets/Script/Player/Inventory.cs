
using System.Collections.Generic;
using System.Text;
using UnityEngine;
public class Inventory : SingletonBehaviour<Inventory>
{
    [HideInInspector] public Slot[] slots; // �κ��丮 ����
    [HideInInspector] public Slot[] mixture; // ���ս���
    const int RESULT_SLOT_INDEX = 4; //resultSlot Index
    public GameObject invnetoryObj; //�κ��丮 ui
    public GameObject mixtureObj;   //���� ui
    public GameObject selectObj;    //���� ui
    public List<ItemData> mixitemList;//���� ������ ����Ʈ 
    private Dictionary<string, int> itemCounts; //���ս��ʿ��� ������ ī���ÿ�
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        itemCounts = new Dictionary<string, int>(5);
        SetArray(ref slots, invnetoryObj); // �κ��丮 �迭
        SetArray(ref mixture, mixtureObj); // ���չ迭
        AddArray(ref slots, selectObj); // �迭�� �߰� ���

    }

    public void SetArray(ref Slot[] array, GameObject targetObj)//Ÿ�ٿ�����Ʈ �迭�� ����
    {
        int size = targetObj.transform.childCount;
        array = new Slot[size];
        for (int i = 0; i < size; i++)
        {
            Transform childTransform = targetObj.transform.GetChild(i);
            array[i] = childTransform.GetComponent<Slot>();
        }
    }
    public void AddArray(ref Slot[] array, GameObject tragetObj)//�迭�� ���ϴ� ������Ʈ solt �߰�
    {
        Slot[] copy = new Slot[array.Length + (tragetObj.transform.childCount)];
        int AddIndex = 0; //�߰��� ������Ʈ ī��Ʈ
        array.CopyTo(copy, 0);
        for (int i = array.Length-1; i < copy.Length; i++)
        {
            Transform childTransform = tragetObj.transform.GetChild(AddIndex);
           
            copy[i] = childTransform.GetComponent<Slot>();
            AddIndex ++;    
        }
        array = new Slot[copy.Length];
        copy.CopyTo(array, 0);
    }
    public void AddItem(ItemData itemData)// ������ �߰�
    {

        bool itemAdded = false;
        int index = -1;
        for (int i = 0; i < slots.Length; i++)//�ߺ������� üũ ���ٸ� ������ �κ��丮 ����տ� �߰�
        {
            // ���� �������� �̹� �ִٸ�
            if (slots[i].itemData != null && slots[i].itemData.itemCode == itemData.itemCode)
            {
                slots[i].AddQuantity();// ���� �߰�
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
            slots[index].SetSlot(itemData);
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
                slots[index].DecCountQuantity(dic.Value);
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
                mixture[4].SetSlot(mixitemList[i]);
                return;
            }
        }
        mixture[RESULT_SLOT_INDEX].Init();
    }
   


}
