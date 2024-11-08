using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MainSSM
{
    public class MonsterSpawner : MonoBehaviour
    {
        private static MonsterSpawner instance; 
        public Transform player;    // 플레이어의 Transform
        public GameObject monsterPrefab;  // 몬스터 프리팹
        public float minRadius = 10f;   // 최소 소환 범위
        public float maxRadius = 15f;  // 최대 소환 범위
        private GameManager gameManager;//게임메니저 싱글톤 GameManager.Instance; 저장용
        private Queue<GameObject> MonsterQueue; //몬스터 프리팹 큐로 저장 오브젝트 풀링용 

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
            float randomRadius = Random.Range(minRadius, maxRadius);

            // 랜덤 위치 계산
            Vector3 spawnPosition = playerPosition + new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle), 0) * randomRadius;

            // 몬스터 소환
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