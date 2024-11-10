using MainSSM;
using System.Collections.Generic;
using Unity.VisualScripting;
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

        if (!objectDictionary.ContainsKey(key))
        {            
            SpawnObjects(key, tilemapWordPos);
        }

    }

    private void SpawnObjects(string key, Vector3 tilemapWordPos)
    {

        Vector3 centerPos = tilemapWordPos;
        List<Vector2> ObjPoss = new List<Vector2>();
        GameObject prefab = (Random.value > 0.6f) ? treePrefab : rockPrefab;
        objectsPerRegion = Random.Range(0, 10);
        if (Random.value > 0.8f)
        {
            for (int i = 0; i < objectsPerRegion; i++) // 포지션 정하는 함수 
            {
                float xRange = Random.Range(centerPos.x - spawnRadius / 13, centerPos.x + spawnRadius / 13);
                float yRange = Random.Range(centerPos.y - spawnRadius / 13, centerPos.y + spawnRadius / 13);

                Vector2 randomPosition = new Vector2(xRange, yRange);
                ObjPoss.Add(randomPosition);
            }
            ObjPoss.Sort((a, b) => b.y.CompareTo(a.y)); // y가 높은순으로 정렬
            for (int i = 0; i < objectsPerRegion; i++) // 생성하는함수 정렬해서 배열
            {
               
                GameObject obj = Instantiate(prefab, ObjPoss[i], Quaternion.identity);
            }

            objectDictionary.Add(key, rockPrefab); // 오브젝트 저장 나무 돌은 배열로 가지고 있기
        }
    }
}
