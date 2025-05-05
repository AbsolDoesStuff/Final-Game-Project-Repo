using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject smallEnemyPrefab;
    [SerializeField] private GameObject largeEnemyPrefab;
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private float minX = 2f;
    [SerializeField] private float maxX = 18f;
    [SerializeField] private float spawnY = 3f;

    private float spawnTimer;

    void Start()
    {
        spawnTimer = spawnInterval;
        if (smallEnemyPrefab == null || largeEnemyPrefab == null)
        {
            Debug.LogError("Enemy Prefabs not assigned in EnemySpawner!");
        }
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            SpawnEnemy();
            spawnTimer = spawnInterval;
        }
    }

    private void SpawnEnemy()
    {
        bool spawnLargeEnemy = Random.Range(0f, 1f) > 0.5f;
        GameObject prefabToSpawn = spawnLargeEnemy ? largeEnemyPrefab : smallEnemyPrefab;

        if (prefabToSpawn == null)
        {
            Debug.LogError("Enemy Prefab not assigned in EnemySpawner!");
            return;
        }

        float randomX = Random.Range(minX, maxX);
        Vector3 spawnPosition = new Vector3(randomX, spawnY, 0f);
        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
    }
}
