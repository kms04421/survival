using MainSSM;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MapObjectSpawner : MonoBehaviour
{
    public GameObject treePrefab;   // ���� ������
    public GameObject rockPrefab;   // �� ������
    public float spawnRadius = 0.1f; // ������Ʈ�� ������ ����
    public int objectsPerRegion = 10; // �� ������ ������Ʈ ��
    private Dictionary<string, GameObject> objectDictionary;
    private Transform player;
    private void Awake()
    {
        
        objectDictionary = new Dictionary<string, GameObject>();
    }
   
    public void LoadObjectsNearPlayer(Vector3Int tilemapPos, Vector3 tilemapWordPos) //���� �÷��̾� ��ġ�� ��ųʸ� Ű���ϰ� �ߺ����� üũ
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
      
        objectDictionary.Add(key, rockPrefab); // ������Ʈ ���� ���� ���� �迭�� ������ �ֱ�
    }

}
