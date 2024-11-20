using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MainSSM
{
    public class EnemySpawner : SingletonBehaviour<EnemySpawner>
    {
        [HideInInspector]public Transform player;    // 플레이어의 Transform
        [HideInInspector]public float monsterSPMinRadius = 10f;   // 최소 소환 범위
        [HideInInspector]public float monsterSPMaxRadius = 15f;  // 최대 소환 범위
        private GameManager gameManager;//게임메니저 싱글톤 GameManager.Instance; 저장용
        public List<GameObject> monsterPrefabs; //몬스터 프리팹 리스트로 저장
        private List<Queue<GameObject>> MonsterQueue; //몬스터 프리팹을 큐 리스트로 저장 오브젝트 풀링용 이유 : 여러 마리의 몬스터를 종류별로 소환하기 위해 
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
                    Enemy enemy = go.GetComponent<Enemy>(); // json으로 가져온 데이터 세팅
                    enemy.enemydata = new EnemyData(JsonDataLoad.enemies[monstr.name]);//딕셔너리에서 이름을 key값으로 세팅

                    queue.Enqueue(go);
                    go.SetActive(false);
                }
                MonsterQueue.Add(queue);
            }
           
            SpawnMonster();
        }
        public void SpawnMonster() // 몬스터 스폰수량
        {
            int spcount = gameManager.roundManager.EnemyCount;
            for (int i = 0; i < spcount; i++)
            {
                MonsterSpawnPosition();
            }
        }

        void MonsterSpawnPosition() // 몬스터 포지션 세팅
        {
            Vector3 playerPosition = player.position;

            // 랜덤 각도와 반경 계산
            float randomAngle = Random.Range(0f, 2f * Mathf.PI); //Mathf.PI 원의 둘래 계산시 사용
            float randomRadius = Random.Range(monsterSPMinRadius, monsterSPMaxRadius);

            // 랜덤 위치 계산
            Vector3 spawnPosition = playerPosition + new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle), 0) * randomRadius;

            int spawnIndex = Random.Range(0, monsterPrefabs.Count);

            // 몬스터 소환
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

        public GameObject SpawnOrSwitchMonster() //몬스터 문제없도록 소환 
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

       
        public void EnqueueMonster(GameObject monster,int spawnIndex) // List<Queue> 의 Queue쪽에 몬스터 오브젝트 다시 추가
        {
            MonsterQueue[spawnIndex].Enqueue(monster);
        }
    }
}