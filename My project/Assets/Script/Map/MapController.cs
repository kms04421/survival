using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public GameObject grid;
    public Transform player;
    private Vector2 tileMapSize;

    private Vector3Int playerGridPosition;
    private Vector3Int lastPlayerGridPosition;

    private List<Transform> tileMaps;

    // Start is called before the first frame update
    void Start()
    {
        tileMaps = new List<Transform>();
        var tile = grid.transform;
    }

    
}
