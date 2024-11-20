using MainSSM;
using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : SingletonBehaviour<ObjectPoolingManager>
{
    public ItemData[] itemDataArray;
    public Dictionary<string, Queue<GameObject>> itemPool;  // 아이템 풀
    public Queue<GameObject> bulletPool; //  
    public GameObject bullctPrefap;
    public Transform items;
    public Transform bullets;

    void Start()
    {
        itemPool = new Dictionary<string, Queue<GameObject>>();
        bulletPool = new Queue<GameObject>();
        items = GameObject.Find("Items").transform;
        CreateItems();
        CreateBullcts();
    }

    // 아이템 풀을 생성
    void CreateItems()
    {
        for (int i = 0; i < itemDataArray.Length; i++)
        {
            Queue<GameObject> itemQueue = new Queue<GameObject>();
            for (int j = 0; j < 50; j++)
            {
                GameObject itemObj = Instantiate(itemDataArray[i].itemPrefab,items);
                itemObj.name = itemDataArray[i].itemPrefab.name;
                Item item = itemObj.GetComponent<Item>();
                item.itemData = itemDataArray[i];
                itemQueue.Enqueue(itemObj);
                itemObj.SetActive(false);
            }
            itemPool.Add(itemDataArray[i].name, itemQueue);
        }
        
    }
    // 총알 풀 생성 
    void CreateBullcts()
    {
        for (int i = 0; i < 40; i++)
        {
           
                GameObject itemObj = Instantiate(bullctPrefap, bullets);
                itemObj.SetActive(false);
                bulletPool.Enqueue(itemObj);
            
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

    public void GetBullet( Transform ChildPos)
    {
        if(bulletPool.Count > 0)
        {
            GameObject go = bulletPool.Dequeue();
            go.transform.rotation = Quaternion.Euler(0,0, ChildPos.parent.rotation.eulerAngles.z - 90);
            go.transform.position = ChildPos.position;
            go.SetActive(true);
        }
    }
}
