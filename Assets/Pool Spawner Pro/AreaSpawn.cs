using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HQ
{
    public class MobileFriendlyWaveSpawner : MonoBehaviour
    {
        [Header("Enemy Settings")]
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private int enemiesPerWave = 5;
        [SerializeField] private int totalWaves = 3;
        [SerializeField] private float timeBetweenWaves = 5f;

        [Header("Spawn Position Settings")]
        [SerializeField] private float spawnRadius = 2f;
        [SerializeField] private float heightOffset = 0.5f;

        [Header("Object Pool Settings")]
        [SerializeField] private int poolSize = 10;
        [SerializeField] private Transform parentObject;

        [SerializeField] private EnemyDeathManager enemyDeathManager; // Reference to EnemyDeathManager

        private Queue<GameObject> enemyPool;
        private int waveIndex = 0;
        private bool isSpawning = false;

        private void Start()
        {
            InitializeObjectPool();
        }

        private void Update()
        {
            if (!isSpawning && waveIndex < totalWaves)
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
                    enemy.transform.SetParent(parentObject);
                }

                enemyPool.Enqueue(enemy);
            }
        }

        // Spawn enemy with randomized position and register it with EnemyDeathManager
        private void SpawnEnemy()
        {
            if (enemyPool.Count > 0)
            {
                GameObject enemy = enemyPool.Dequeue();

                Vector3 randomOffset = Random.insideUnitSphere * spawnRadius;
                randomOffset.y = 0;
                Vector3 spawnPosition = spawnPoint.position + randomOffset + new Vector3(0, heightOffset, 0);

                enemy.transform.position = spawnPosition;
                enemy.transform.rotation = spawnPoint.rotation;
                enemy.SetActive(true);

                // Register the new enemy with EnemyDeathManager
                EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();
                if (enemyStats != null && enemyDeathManager != null)
                {
                    enemyDeathManager.RegisterNewEnemy(enemyStats);
                }
            }
        }

        // Coroutine to spawn a wave of enemies
        IEnumerator SpawnWave()
        {
            isSpawning = true;

            for (int i = 0; i < enemiesPerWave; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(1f);
            }

            waveIndex++;
            isSpawning = false;
        }
    }
}
