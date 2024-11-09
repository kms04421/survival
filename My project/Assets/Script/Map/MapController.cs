using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace MainSSM
{
    public class MapController : SingletonBehaviour<MapController>
    {
        public GameObject grid; //전체 타일맵 관리하는 Grid
        [HideInInspector]public Transform player; // 플레이어 위치 저장용

        private Vector2 tilemapSize; // 타일맵 크기 
        private Vector3Int playerGridPosition;// 현재 플레이어 그리드 좌표
        private Vector3Int lastPlayerGridPosition; // 이전 프레이임의 플레이어 그리드 좌표 
        private Queue<Transform> tileMaps;
        private WaitForSeconds updateSecond;
        private MapObjectSpawner mapObjectSpawner;
        public int spawnInterval = 5;
        // Start is called before the first frame update
        void Awake()
        {
            mapObjectSpawner = GetComponent<MapObjectSpawner>();
            player = GameManager.Instance.PlayerPosition;
            updateSecond = new WaitForSeconds(0.2f);
            Transform tile = grid.transform.GetChild(0);
            tileMaps = new Queue<Transform>();
            tilemapSize = CalculateTileSize(tile.GetComponent<Tilemap>()); // 타일맵 크기 계산 함수 호출


            for (int i = 0; i < 9; i++) //타일맵 생성후 비활성화
            {
                Transform newTilemap = Instantiate(tile, grid.transform);
                newTilemap.gameObject.SetActive(false);
                tileMaps.Enqueue(newTilemap); // 타일맵 리스트 추가
            }

            playerGridPosition = GetGridPosition(player.position);//플레이어 위치의 그리드 좌표 
            lastPlayerGridPosition = playerGridPosition; //플레이어의 마지막 그리드 위치 좌표

         
            StartCoroutine("MapUpdate");

        }// 플레이어 주변 타일맵을 활성화
        private void Start()
        {

            ActivateTilemapsAroundPlayer();
        }
        void ActivateTilemapsAroundPlayer()//플레이어 주변위치에 타일 배치
        {
            int[,] offsets = { { 0, 0 }, { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 },
                       { 1, 1 }, { -1, -1 }, { 1, -1 }, { -1, 1 } }; // 주변 타일 위치 

            HashSet<Vector3Int> activeGridPositions = new HashSet<Vector3Int>();

           
            for (int i = 0; i < tileMaps.Count; i++)
            {
                Vector3Int gridPos = playerGridPosition + new Vector3Int(offsets[i, 0], offsets[i, 1], 0); // 플레이어 위치에 타일 위치 구하기

                if (activeGridPositions.Contains(gridPos))//List 대신 HashSet을 사용하면 Contains가 더빠름 O(1)
                    continue;

                if (tileMaps.Count > 0)
                {
                    Transform tilemap = tileMaps.Dequeue();
                    Vector3 teilPos = new Vector3(gridPos.x * tilemapSize.x, gridPos.y * tilemapSize.y, 0);
                    tilemap.position = teilPos;
                    tilemap.gameObject.SetActive(true);
                    tileMaps.Enqueue(tilemap);
                    activeGridPositions.Add(gridPos);
                   
                    mapObjectSpawner.LoadObjectsNearPlayer(gridPos , teilPos);
                    
                }
            }
        }
        IEnumerator MapUpdate()
        {
            while (true)
            {
                playerGridPosition = GetGridPosition(player.position);//현재 플레이어의 위치
              
                if (playerGridPosition != lastPlayerGridPosition)//플레이어가 이동할경우
                {                  
                     ActivateTilemapsAroundPlayer();
                    // MoveTilemaps(playerGridPosition - lastPlayerGridPosition); //타일맵 이동
                    lastPlayerGridPosition = playerGridPosition; //이전위치 업데이트
                }
                yield return updateSecond;
            }
        }
       
        public Vector3Int GetGridPosition(Vector3 position) //월드 좌표를 그리드 좌표로 변환
        {
            return new Vector3Int(
                Mathf.RoundToInt(position.x / tilemapSize.x), //x 좌표를 타일맵 크기로 나누어 그리드 좌표 계산 
                Mathf.RoundToInt(position.y / tilemapSize.y), //y 좌표를 타일맵 크기로 나누어 그리드 좌표 계산
                0
                );
        }
        


        // 타일맵 크기 계산
        private Vector2 CalculateTileSize(Tilemap tilemap)
        {
            BoundsInt bounds = tilemap.cellBounds;
            return new Vector2(bounds.size.x, bounds.size.y); // 타일맵 크기 반환

        }
    }
}