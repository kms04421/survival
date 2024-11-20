using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MainSSM
{
    public class EnemySpawner : SingletonBehaviour<EnemySpawner>
    {
        [HideInInspector]public Transform player;    // �÷��̾��� Transform
        [HideInInspector]public float monsterSPMinRadius = 10f;   // �ּ� ��ȯ ����
        [HideInInspector]public float monsterSPMaxRadius = 15f;  // �ִ� ��ȯ ����
        private GameManager gameManager;//���Ӹ޴��� �̱��� GameManager.Instance; �����
        public List<GameObject> monsterPrefabs; //���� ������ ����Ʈ�� ����
        private List<Queue<GameObject>> MonsterQueue; //���� �������� ť ����Ʈ�� ���� ������Ʈ Ǯ���� ���� : ���� ������ ���͸� �������� ��ȯ�ϱ� ���� 
        WaitForSeconds forSeconds;
      
        void Start()
        {
            forSeconds = new WaitForSeconds(1f);
            MonsterQueue = new List<Queue<GameObject>>();
            gameManager = GameManager.Instance;
            player = gameManager.PlayerPosition;
            foreach (GameObject monster in monsterPrefabs)
            {
                GameObject monstr = new GameObject();
                monstr.name = monster.name;
                monstr.transform.parent = gameObject.transform;
               
                Queue<GameObject> queue = new Queue<GameObject>();
                for (int i = 0; i < 100; i++)
                {
                    GameObject go = Instantiate(monster, monstr.transform);
                    Enemy enemy = go.GetComponent<Enemy>(); // json���� ������ ������ ����
                    enemy.enemydata = new EnemyData(JsonDataLoad.enemies[monstr.name]);//��ųʸ����� �̸��� key������ ����

                    queue.Enqueue(go);
                    go.SetActive(false);
                }
                MonsterQueue.Add(queue);
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
            float randomRadius = Random.Range(monsterSPMinRadius, monsterSPMaxRadius);

            // ���� ��ġ ���
            Vector3 spawnPosition = playerPosition + new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle), 0) * randomRadius;

            int spawnIndex = Random.Range(0, monsterPrefabs.Count);

            // ���� ��ȯ
            GameObject go;
            if (MonsterQueue[spawnIndex].Count > 0)
            { 
                go = MonsterQueue[spawnIndex].Dequeue();
            }
            else
            {
               go  = SpawnOrSwitchMonster();
            }  
           

            if (go == null) return;
            go.GetComponent<Enemy>().spawnIndex = spawnIndex;
            go.transform.position = spawnPosition;
            go.SetActive(true);
        }

        public GameObject SpawnOrSwitchMonster() //���� ���������� ��ȯ 
        {
            for (int i = 0; i < monsterPrefabs.Count; i++) 
            {
                if(0 < MonsterQueue[i].Count)
                {
                    return MonsterQueue[i].Dequeue();

                }               
            }
            return null;
        }

       
        public void EnqueueMonster(GameObject monster,int spawnIndex) // List<Queue> �� Queue�ʿ� ���� ������Ʈ �ٽ� �߰�
        {
            MonsterQueue[spawnIndex].Enqueue(monster);
        }
    }
}