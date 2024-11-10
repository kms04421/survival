using MainSSM;
using System.Collections.Generic;
using Unity.VisualScripting;
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
            for (int i = 0; i < objectsPerRegion; i++) // ������ ���ϴ� �Լ� 
            {
                float xRange = Random.Range(centerPos.x - spawnRadius / 13, centerPos.x + spawnRadius / 13);
                float yRange = Random.Range(centerPos.y - spawnRadius / 13, centerPos.y + spawnRadius / 13);

                Vector2 randomPosition = new Vector2(xRange, yRange);
                ObjPoss.Add(randomPosition);
            }
            ObjPoss.Sort((a, b) => b.y.CompareTo(a.y)); // y�� ���������� ����
            for (int i = 0; i < objectsPerRegion; i++) // �����ϴ��Լ� �����ؼ� �迭
            {
               
                GameObject obj = Instantiate(prefab, ObjPoss[i], Quaternion.identity);
            }

            objectDictionary.Add(key, rockPrefab); // ������Ʈ ���� ���� ���� �迭�� ������ �ֱ�
        }
    }
}
