using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileFriendlyWaveSpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private int enemiesPerWave = 5;
    [SerializeField] private int totalWaves = 3;
    [SerializeField] private float timeBetweenWaves = 5f;
    
    [Header("Spawn Position Settings")]
    [SerializeField] private float spawnRadius = 2f; // Range for random spawn positions around spawn point
    [SerializeField] private float heightOffset = 0.5f; // Adjust spawn height

    [Header("Object Pool Settings")]
    [SerializeField] private int poolSize = 10;
    [SerializeField] private Transform parentObject; // Parent for organization in hierarchy

    private Queue<GameObject> enemyPool;
    private int waveIndex = 0;
    private bool isSpawning = false;

    private void Start()
    {
        InitializeObjectPool();
    }

    private void Update()
    {
        if (!isSpawning && waveIndex < totalWaves && IsSpawnPointOutOfView())
        {
            StartCoroutine(SpawnWave());
        }
    }

    // Initialize object pool with inactive enemy objects
    private void InitializeObjectPool()
    {
        enemyPool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false);

            if (parentObject)
            {
                enemy.transform.SetParent(parentObject); // Set parent for better hierarchy organization
            }

            enemyPool.Enqueue(enemy);
        }
    }

    // Spawn enemy with randomized position and parent assignment
    private void SpawnEnemy()
    {
        if (enemyPool.Count > 0)
        {
            GameObject enemy = enemyPool.Dequeue();

            // Calculate random spawn position around the spawn point
            Vector3 randomOffset = Random.insideUnitSphere * spawnRadius;
            randomOffset.y = 0; // Maintain same Y-axis level for horizontal spawning
            Vector3 spawnPosition = spawnPoint.position + randomOffset + new Vector3(0, heightOffset, 0);

            enemy.transform.position = spawnPosition;
            enemy.transform.rotation = spawnPoint.rotation;
            enemy.SetActive(true);
        }
    }

    // Return the enemy to the object pool after "defeated"
    public void ReturnEnemyToPool(GameObject enemy)
    {
        enemy.SetActive(false);

        // Ensure the enemy is re-parented to the pool for organization
        if (parentObject)
        {
            enemy.transform.SetParent(parentObject);
        }

        enemyPool.Enqueue(enemy);
    }

    // Coroutine to spawn a wave of enemies
    IEnumerator SpawnWave()
    {
        isSpawning = true;

        for (int i = 0; i < enemiesPerWave; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1f); // Optional: Delay between spawns
        }

        waveIndex++;
        isSpawning = false;
    }

    // Check if the camera is not looking at the spawn point
    private bool IsSpawnPointOutOfView()
    {
        Vector3 cameraToSpawn = (spawnPoint.position - Camera.main.transform.position).normalized;
        float dotProduct = Vector3.Dot(Camera.main.transform.forward, cameraToSpawn);
        return dotProduct < 0;
    }
}
