using UnityEngine;

public class ScriptTileHandler : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] obstaclePrefabs;
    public GameObject coinPrefab;
    public GameObject[] powerUpPrefabs;

    [Range(0f, 1f)] public float obstacleChance = 0.35f;
    [Range(0f, 1f)] public float coinChance = 0.45f;
    [Range(0f, 1f)] public float powerUpChance = 0.04f;

    public void Populate(bool allowObstacles)
    {
        foreach (Transform pt in spawnPoints)
        {
            float roll = Random.value;

            if (allowObstacles && roll < obstacleChance && obstaclePrefabs.Length > 0)
                Spawn(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)], pt);
            else if (roll < obstacleChance + coinChance && coinPrefab != null)
            {
                Spawn(coinPrefab, pt);
            }
            else if (roll < obstacleChance + coinChance + powerUpChance && powerUpPrefabs.Length > 0)
                Spawn(powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)], pt);
        }
    }

    void Spawn(GameObject prefab, Transform pt)
    {
        Instantiate(prefab, pt.position, pt.rotation, transform);
    }

}
