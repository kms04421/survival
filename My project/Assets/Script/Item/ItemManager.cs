using MainSSM;
using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : SingletonBehaviour<ItemManager>
{
    public ItemData[] itemDataArray;
    private Dictionary<string, Queue<GameObject>> itemPool;  // 아이템 풀
    public Transform items;

    void Start()
    {
        itemPool = new Dictionary<string, Queue<GameObject>>();
        items = GameObject.Find("Items").transform;
        InitializeItemPool();
    }

    // 아이템 풀을 초기화
    void InitializeItemPool()
    {
        for (int i = 0; i < itemDataArray.Length; i++)
        {
            Queue<GameObject> itemQueue = new Queue<GameObject>();
            for (int j = 0; j < 10; j++)
            {
                GameObject itemObj = Instantiate(itemDataArray[i].itemPrefab,items);
                Item item = itemObj.GetComponent<Item>();
                item.itemData = itemDataArray[i];
                itemQueue.Enqueue(itemObj);
                itemObj.SetActive(false);
            }
            itemPool.Add(itemDataArray[i].name, itemQueue);
        }
        
    }

    // 아이템을 가져오는 함수 
    public GameObject GetItem(string name)
    {
        if (itemPool[name].Count > 0)
        {
            return itemPool[name].Dequeue();
        }
        return null;  // 풀에 아이템이 없을 경우
    }
}
