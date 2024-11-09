using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace MainSSM
{
    public class MapController : SingletonBehaviour<MapController>
    {
        public GameObject grid; //��ü Ÿ�ϸ� �����ϴ� Grid
        [HideInInspector]public Transform player; // �÷��̾� ��ġ �����

        private Vector2 tilemapSize; // Ÿ�ϸ� ũ�� 
        private Vector3Int playerGridPosition;// ���� �÷��̾� �׸��� ��ǥ
        private Vector3Int lastPlayerGridPosition; // ���� ���������� �÷��̾� �׸��� ��ǥ 
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
            tilemapSize = CalculateTileSize(tile.GetComponent<Tilemap>()); // Ÿ�ϸ� ũ�� ��� �Լ� ȣ��


            for (int i = 0; i < 9; i++) //Ÿ�ϸ� ������ ��Ȱ��ȭ
            {
                Transform newTilemap = Instantiate(tile, grid.transform);
                newTilemap.gameObject.SetActive(false);
                tileMaps.Enqueue(newTilemap); // Ÿ�ϸ� ����Ʈ �߰�
            }

            playerGridPosition = GetGridPosition(player.position);//�÷��̾� ��ġ�� �׸��� ��ǥ 
            lastPlayerGridPosition = playerGridPosition; //�÷��̾��� ������ �׸��� ��ġ ��ǥ

         
            StartCoroutine("MapUpdate");

        }// �÷��̾� �ֺ� Ÿ�ϸ��� Ȱ��ȭ
        private void Start()
        {

            ActivateTilemapsAroundPlayer();
        }
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
                playerGridPosition = GetGridPosition(player.position);//���� �÷��̾��� ��ġ
              
                if (playerGridPosition != lastPlayerGridPosition)//�÷��̾ �̵��Ұ��
                {                  
                     ActivateTilemapsAroundPlayer();
                    // MoveTilemaps(playerGridPosition - lastPlayerGridPosition); //Ÿ�ϸ� �̵�
                    lastPlayerGridPosition = playerGridPosition; //������ġ ������Ʈ
                }
                yield return updateSecond;
            }
        }
       
        public Vector3Int GetGridPosition(Vector3 position) //���� ��ǥ�� �׸��� ��ǥ�� ��ȯ
        {
            return new Vector3Int(
                Mathf.RoundToInt(position.x / tilemapSize.x), //x ��ǥ�� Ÿ�ϸ� ũ��� ������ �׸��� ��ǥ ��� 
                Mathf.RoundToInt(position.y / tilemapSize.y), //y ��ǥ�� Ÿ�ϸ� ũ��� ������ �׸��� ��ǥ ���
                0
                );
        }
        


        // Ÿ�ϸ� ũ�� ���
        private Vector2 CalculateTileSize(Tilemap tilemap)
        {
            BoundsInt bounds = tilemap.cellBounds;
            return new Vector2(bounds.size.x, bounds.size.y); // Ÿ�ϸ� ũ�� ��ȯ

        }
    }
}