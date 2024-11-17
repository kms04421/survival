
using System.Collections.Generic;
using System.Text;
using UnityEngine;
public class Inventory : SingletonBehaviour<Inventory>
{
    [HideInInspector] public Slot[] slots;
    [HideInInspector] public Slot[] mixture;

    private Dictionary<string, int> itemCounts;
    public GameObject invnetoryObj; //인베토리 ui
    public GameObject mixtureObj;   //조합 ui
    public GameObject selectObj;    //선택 ui
    public List<ItemData> mixitemList;//조합 아이템 리스트 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        itemCounts = new Dictionary<string, int>();
        SetArray(ref slots, invnetoryObj); // 인벤토리 배열
        SetArray(ref mixture, mixtureObj); // 조합배열
        AddArray(ref slots, selectObj);

    }

    public void SetArray(ref Slot[] array, GameObject tragetObj)//타겟오브젝트 배열에 저장
    {
        int size = tragetObj.transform.childCount;
        array = new Slot[size];
        for (int i = 0; i < size; i++)
        {
            Transform invnetoryObjtransform = tragetObj.transform.GetChild(i);
            array[i] = invnetoryObjtransform.GetComponent<Slot>();
        }
    }
    public void AddArray(ref Slot[] array, GameObject tragetObj)//배열에 원하는 오브젝트 solt 추가
    {
        Slot[] copy = new Slot[array.Length + (tragetObj.transform.childCount-1)];
        int AddIndex = 0; //추가할 오브젝트 카운트
        array.CopyTo(copy, 0);
        for (int i = array.Length-1; i < copy.Length; i++)
        {
            Transform Objtransform = tragetObj.transform.GetChild(AddIndex);
           
            copy[i] = Objtransform.GetComponent<Slot>();
            AddIndex ++;    
        }
        array = new Slot[copy.Length];
        copy.CopyTo(array, 0);
    }
    public void AddItem(ItemData itemData)// 아이템 추가
    {

        bool itemAdded = false;
        int index = -1;
        for (int i = 0; i < slots.Length; i++)//중복아이템 체크 없다면 아이템 인벤토리 가장앞에 추가
        {
            // 같은 아이템이 이미 있다면
            if (slots[i].itemData != null && slots[i].itemData.itemCode == itemData.itemCode)
            {
                slots[i].AddQuantity();// 수량 추가
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
            slots[index].SetSlot(itemData);
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
                slots[index].DecCountQuantity(dic.Value);
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
                mixture[4].SetSlot(mixitemList[i]);
                return;
            }
        }
        mixture[4].Init();
    }
   


}
