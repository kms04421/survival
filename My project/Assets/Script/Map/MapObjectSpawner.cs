using MainSSM;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MapObjectSpawner : MonoBehaviour
{
    public GameObject treePrefab;   // 나무 프리팹
    public GameObject rockPrefab;   // 돌 프리팹
    public float spawnRadius = 0.1f; // 오브젝트를 생성할 범위
    public int objectsPerRegion = 10; // 각 영역당 오브젝트 수
    private Dictionary<string, GameObject> objectDictionary;
    private Transform player;
    private void Awake()
    {
        
        objectDictionary = new Dictionary<string, GameObject>();
    }
   
    public void LoadObjectsNearPlayer(Vector3Int tilemapPos, Vector3 tilemapWordPos) //현재 플레이어 위치로 딕셔너리 키정하고 중복여부 체크
    {
   
        string key = $"{tilemapPos.x}_{tilemapPos.y}";
        Debug.Log(key);
        if (!objectDictionary.ContainsKey(key))
        {            
            SpawnObjects(key, tilemapWordPos);
        }

    }

    private void SpawnObjects(string key, Vector3 tilemapWordPos)
    {
        
        Vector3 centerPos = tilemapWordPos;
        if(Random.value > 0.6f)
        {
            for (int i = 0; i < objectsPerRegion; i++)
            {
                float xRange = Random.Range(centerPos.x - spawnRadius / 10, centerPos.x + spawnRadius / 10);
                float yRange = Random.Range(centerPos.y - spawnRadius / 10, centerPos.y + spawnRadius / 10);

                Vector2 randomPosition = new Vector2(xRange, yRange);
                GameObject prefab = (Random.value > 0.5f) ? treePrefab : rockPrefab;
                GameObject obj = Instantiate(prefab, randomPosition, Quaternion.identity);

            }
        }
      
        objectDictionary.Add(key, rockPrefab); // 오브젝트 저장 나무 돌은 배열로 가지고 있기
    }

}
