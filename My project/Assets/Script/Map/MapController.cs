using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class MapController : MonoBehaviour
{
    public GameObject grid; //��ü Ÿ�ϸ� �����ϴ� Grid
    public Transform player; // �÷��̾� ��ġ �����

    private Vector2 tilemapSize; // Ÿ�ϸ� ũ�� 
    private Vector3Int playerGridPosition;// ���� �÷��̾� �׸��� ��ǥ
    private Vector3Int lastPlayerGridPosition; // ���� ���������� �÷��̾� �׸��� ��ǥ 
    private Queue<Transform> tileMaps;

    // Start is called before the first frame update
    void Start()
    {
        Transform tile = grid.transform.GetChild(0);
        tileMaps = new Queue<Transform>();
        tilemapSize = CalculateTileSize(tile.GetComponent<Tilemap>()); // Ÿ�ϸ� ũ�� ��� �Լ� ȣ��


        for(int i= 0; i< 9; i++) //Ÿ�ϸ� ������ ��Ȱ��ȭ
        {
            Transform newTilemap = Instantiate(tile,grid.transform);
            newTilemap.gameObject.SetActive(false);
            tileMaps.Enqueue(newTilemap); // Ÿ�ϸ� ����Ʈ �߰�
        }


        playerGridPosition = GetGridPosition(player.position);//�÷��̾� ��ġ�� �׸��� ��ǥ 
        lastPlayerGridPosition = playerGridPosition; //�÷��̾��� ������ �׸��� ��ġ ��ǥ

        ActivateTilemapsAroundPlayer();
    }// �÷��̾� �ֺ� Ÿ�ϸ��� Ȱ��ȭ
    void ActivateTilemapsAroundPlayer()//�÷��̾� �ֺ���ġ�� Ÿ�� ��ġ
    {
        int[,] offsets = { { 0, 0 }, { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 },
                       { 1, 1 }, { -1, -1 }, { 1, -1 }, { -1, 1 } }; // �ֺ� Ÿ�� ��ġ 

        HashSet<Vector3Int> activeGridPositions = new HashSet<Vector3Int>();

        for (int i = 0; i < tileMaps.Count; i++)
        {     
            Vector3Int gridPos = playerGridPosition + new Vector3Int(offsets[i, 0], offsets[i, 1], 0); // �÷��̾� ��ġ�� Ÿ�� ��ġ ���ϱ�

            if (activeGridPositions.Contains(gridPos))//List ��� HashSet�� ����ϸ� Contains�� ������ O(1)
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
        playerGridPosition = GetGridPosition(player.position);//���� �÷��̾��� ��ġ

        if(playerGridPosition != lastPlayerGridPosition)//�÷��̾ �̵��Ұ��
        {
            ActivateTilemapsAroundPlayer();
           // MoveTilemaps(playerGridPosition - lastPlayerGridPosition); //Ÿ�ϸ� �̵�
            lastPlayerGridPosition = playerGridPosition; //������ġ ������Ʈ
        }
    }
    //���� ��ǥ�� �׸��� ��ǥ�� ��ȯ
    private Vector3Int GetGridPosition(Vector3 position)
    {
        return new Vector3Int(
            Mathf.RoundToInt(position.x / tilemapSize.x), //x ��ǥ�� Ÿ�ϸ� ũ��� ������ �׸��� ��ǥ ��� 
            Mathf.RoundToInt(position.y / tilemapSize.y), //y ��ǥ�� Ÿ�ϸ� ũ��� ������ �׸��� ��ǥ ���
            0
            );
    }
    //Ư�� �׸��� ��ġ�� �̵� 
 /*   private void MoveTilemaps(Vector3Int gridOffset)
    {
        foreach (Transform tilemap in tileMaps)
        {
            tilemap.position += new Vector3(gridOffset.x * tilemapSize.x, gridOffset.y * tilemapSize.y, 0);
        }
    }*/
    // Ÿ�ϸ� ũ�� ���
    private Vector2 CalculateTileSize(Tilemap tilemap)
    {
        BoundsInt bounds = tilemap.cellBounds;
        return new Vector2(bounds.size.x, bounds.size.y); // Ÿ�ϸ� ũ�� ��ȯ

    }
}
