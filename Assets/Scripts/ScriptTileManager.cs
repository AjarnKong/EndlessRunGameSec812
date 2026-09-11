using UnityEngine;
using System.Collections.Generic;

public class ScriptTileManager : MonoBehaviour
{
    public Transform player;
    public GameObject[] tilePrefabs;
    public float tileLength = 20f;
    public int tilesOnScreen = 6;
    public int safeTiles = 2;

    private float nextSpawnZ = 0f;
    private int tileSpawned = 0;
    private readonly Queue<GameObject> activeTiles = new Queue<GameObject>();

    void Start()
    {
        for (int i = 0; i < tilesOnScreen; i++)
        {
            SpawnTile();
        }
    }

    void Update()
    {
        float triggerZ = nextSpawnZ - (tilesOnScreen -1) * tileLength;
        if (player.position.z > triggerZ)
        {
            SpawnTile();
            RecycleOldestTile();
        }
    }

    void SpawnTile() 
    { 
        GameObject tile = Instantiate(tilePrefabs[Random.Range(0, tilePrefabs.Length)], 
            new Vector3(0, 0, nextSpawnZ), Quaternion.identity, transform);

        tile.GetComponent<ScriptTileHandler>()?.Populate(tileSpawned >= safeTiles);

        activeTiles.Enqueue(tile);

        nextSpawnZ += tileLength;

        tileSpawned++;
    }

    void RecycleOldestTile()
    {
        if (activeTiles.Count > tilesOnScreen)
        {
            Destroy(activeTiles.Dequeue());
        }
    }
}
