using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MainSSM
{
    public class MonsterSpawner : MonoBehaviour
    {
        private static MonsterSpawner instance; 
        public Transform player;    // �÷��̾��� Transform
        public GameObject monsterPrefab;  // ���� ������
        public float minRadius = 10f;   // �ּ� ��ȯ ����
        public float maxRadius = 15f;  // �ִ� ��ȯ ����
        private GameManager gameManager;//���Ӹ޴��� �̱��� GameManager.Instance; �����
        private Queue<GameObject> MonsterQueue; //���� ������ ť�� ���� ������Ʈ Ǯ���� 

        public static MonsterSpawner Instance
        {
            get
            {
                if (instance == null)
                {
                    return null;
                }
                return instance;
            }
        }
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        void Start()
        {
            MonsterQueue = new Queue<GameObject>();
            gameManager = GameManager.Instance;
            player = gameManager.PlayerPosition;
            for (int i = 0; i < 100; i++)
            {
                GameObject go = Instantiate(monsterPrefab, gameObject.transform);
                MonsterQueue.Enqueue(go);
                go.SetActive(false);
            }
            SpawnMonster();
        }
        public void SpawnMonster() // ���� ��������
        {
            int spcount = gameManager.roundManager.EnemyCount;
            for (int i = 0; i < spcount; i++)
            {
                MonsterSpawnPosition();
            }
        }

        void MonsterSpawnPosition() // ���� ������ ����
        {
            Vector3 playerPosition = player.position;

            // ���� ������ �ݰ� ���
            float randomAngle = Random.Range(0f, 2f * Mathf.PI); //Mathf.PI ���� �ѷ� ���� ���
            float randomRadius = Random.Range(minRadius, maxRadius);

            // ���� ��ġ ���
            Vector3 spawnPosition = playerPosition + new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle), 0) * randomRadius;

            // ���� ��ȯ
            GameObject go = MonsterQueue.Dequeue();
            go.transform.position = spawnPosition;
            go.SetActive(true);
        }
        public void EnqueueMonster(GameObject monster)
        {
            MonsterQueue.Enqueue(monster);
        }
    }
}