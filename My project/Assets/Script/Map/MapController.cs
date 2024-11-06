using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class MapController : MonoBehaviour
{
    public GameObject grid; //전체 타일맵 관리하는 Grid
    public Transform player; // 플레이어 위치 저장용

    private Vector2 tilemapSize; // 타일맵 크기 
    private Vector3Int playerGridPosition;// 현재 플레이어 그리드 좌표
    private Vector3Int lastPlayerGridPosition; // 이전 프레이임의 플레이어 그리드 좌표 
    private Queue<Transform> tileMaps;

    // Start is called before the first frame update
    void Start()
    {
        Transform tile = grid.transform.GetChild(0);
        tileMaps = new Queue<Transform>();
        tilemapSize = CalculateTileSize(tile.GetComponent<Tilemap>()); // 타일맵 크기 계산 함수 호출


        for(int i= 0; i< 9; i++) //타일맵 생성후 비활성화
        {
            Transform newTilemap = Instantiate(tile,grid.transform);
            newTilemap.gameObject.SetActive(false);
            tileMaps.Enqueue(newTilemap); // 타일맵 리스트 추가
        }


        playerGridPosition = GetGridPosition(player.position);//플레이어 위치의 그리드 좌표 
        lastPlayerGridPosition = playerGridPosition; //플레이어의 마지막 그리드 위치 좌표

        ActivateTilemapsAroundPlayer();
    }// 플레이어 주변 타일맵을 활성화
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
                tilemap.position = new Vector3(gridPos.x * tilemapSize.x, gridPos.y * tilemapSize.y, 0);
                tilemap.gameObject.SetActive(true);
                tileMaps.Enqueue(tilemap);

                activeGridPositions.Add(gridPos);
            }
        }
    }
    private void Update()
    {
        playerGridPosition = GetGridPosition(player.position);//현재 플레이어의 위치

        if(playerGridPosition != lastPlayerGridPosition)//플레이어가 이동할경우
        {
            ActivateTilemapsAroundPlayer();
           // MoveTilemaps(playerGridPosition - lastPlayerGridPosition); //타일맵 이동
            lastPlayerGridPosition = playerGridPosition; //이전위치 업데이트
        }
    }
    //월드 좌표를 그리드 좌표로 변환
    private Vector3Int GetGridPosition(Vector3 position)
    {
        return new Vector3Int(
            Mathf.RoundToInt(position.x / tilemapSize.x), //x 좌표를 타일맵 크기로 나누어 그리드 좌표 계산 
            Mathf.RoundToInt(position.y / tilemapSize.y), //y 좌표를 타일맵 크기로 나누어 그리드 좌표 계산
            0
            );
    }
    //특정 그리드 위치로 이동 
 /*   private void MoveTilemaps(Vector3Int gridOffset)
    {
        foreach (Transform tilemap in tileMaps)
        {
            tilemap.position += new Vector3(gridOffset.x * tilemapSize.x, gridOffset.y * tilemapSize.y, 0);
        }
    }*/
    // 타일맵 크기 계산
    private Vector2 CalculateTileSize(Tilemap tilemap)
    {
        BoundsInt bounds = tilemap.cellBounds;
        return new Vector2(bounds.size.x, bounds.size.y); // 타일맵 크기 반환

    }
}
