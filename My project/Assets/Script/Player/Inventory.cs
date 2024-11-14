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
    public List<ItemData> mixitemList;//조합 아이템 리스트 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        itemCounts = new Dictionary<string, int>();
        SetArray(ref slots, invnetoryObj, 33); // 인벤토리 배열
        SetArray(ref mixture, mixtureObj, 5); // 조합배열

    }

    public void SetArray(ref Slot[] array, GameObject tragetObj, int size)//타겟오브젝트 배열에 저장
    {
        array = new Slot[size];
        int invnetoryCount = tragetObj.transform.childCount;
        for (int i = 0; i < invnetoryCount; i++)
        {
            Transform invnetoryObjtransform = tragetObj.transform.GetChild(i);
            array[i] = invnetoryObjtransform.GetComponent<Slot>();
        }
    }

    public void AddItem(ItemData itemData)// 아이템 추가
    {

        bool itemAdded = false;
        int index = -1;
        for (int i = 0; i < slots.Length; i++)
        {
            // 같은 아이템이 이미 있다면
            if (slots[i].itemData != null && slots[i].itemData.name == itemData.name)
            {

                slots[i].quantity++;
                slots[i].textMeshPro.text = "x" + slots[i].quantity.ToString();
                itemAdded = false;
                break;
            }

            // 빈 슬롯이 있다면 해당 슬롯에 아이템 추가
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
            slots[index].quantity = 1;  // 아이템을 추가할 때 초기 수량은 1로 설정
            slots[index].textMeshPro.text = "x" + slots[index].quantity.ToString();
        }
    }

    public int ItemSerch(string code) // 인벤토리에서 아이템 탐색
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

    public bool MinItem() //조합시 아이템 빼기
    {
        bool isbool = true;
        itemCounts.Clear(); // 기존 데이터 초기화

        for (int i = 0; i < mixture.Length-1; i++)  // 각 아이템의 개수를 Dictionary로 추가
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
               
                if (slots[ItemSerch(mixture[i].itemData.itemCode)].quantity < itemCounts[mixture[i].itemData.itemCode]) //조합수량 보다 많은지 체크
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

    public string MixCodeSum() // 아이템 코드 조합
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
    public void CraftItem() // 조합 아이템 검색후 반환
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
