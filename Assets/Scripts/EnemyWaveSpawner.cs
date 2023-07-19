using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string name;
        public List<EnemySetup> enemyPrefabs;
        public float spawnInterval;
    }

    [Header("Enemy Wave Variables")]
    public Wave[] waves;
    private int currentWaveIndex = 0; // Current wave index

    public int numberOfEnemiesToSpawn;  // Number of enemies to spawn
    [SerializeField] float spawnDelay;  // Delay between enemy spawns
    [SerializeField] GameObject spawnEffect;
    [SerializeField] Transform player;  // Reference to the player's transform
    public SpawnMethod enemySpawnMethod = SpawnMethod.RoundRobin;  // Method for spawning enemies

    private NavMeshTriangulation triangulation;  // NavMesh triangulation data
    private Dictionary<int, ObjectPool> enemyObjectPools = new();  // Dictionary of enemy object pools

    private void Awake()
    {
        // Create object pools for each enemy prefab
        for (int i = 0; i < waves.Length; i++)
        {
            foreach (var enemyPrefab in waves[i].enemyPrefabs)
            {
                if (!enemyObjectPools.ContainsKey(enemyPrefab.GetInstanceID()))
                {
                    enemyObjectPools.Add(enemyPrefab.GetInstanceID(), ObjectPool.CreateInstance(enemyPrefab, numberOfEnemiesToSpawn));
                }
            }
        }
    }

    private void Start()
    {
        triangulation = NavMesh.CalculateTriangulation();  // Calculate NavMesh triangulation data
        StartNextWave(); // Start the first wave
    }

    private IEnumerator SpawnEnemies(Wave wave)
    {
        WaitForSeconds wait = new WaitForSeconds(spawnDelay);

        int spawnedEnemies = 0;

        while (spawnedEnemies < numberOfEnemiesToSpawn)
        {
            if (enemySpawnMethod == SpawnMethod.RoundRobin)
            {
                SpawnRoundRobinEnemy(wave, spawnedEnemies);
            }
            else if (enemySpawnMethod == SpawnMethod.Random)
            {
                SpawnRandomEnemy(wave);
            }
            spawnedEnemies++;

            yield return wait;
        }

        yield return new WaitForSeconds(wave.spawnInterval);
        StartNextWave();
    }

    private void SpawnRoundRobinEnemy(Wave wave, int spawnedEnemies)
    {
        int spawnIndex = wave.enemyPrefabs.Count > 0 ? spawnedEnemies % wave.enemyPrefabs.Count : -1;

        if (spawnIndex >= 0)
        {
            EnemySetup enemyPrefab = wave.enemyPrefabs[spawnIndex];
            DoSpawnEnemy(enemyPrefab);
        }
    }

    private void SpawnRandomEnemy(Wave wave)
    {
        if (wave.enemyPrefabs.Count > 0)
        {
            int spawnIndex = Random.Range(0, wave.enemyPrefabs.Count);
            EnemySetup enemyPrefab = wave.enemyPrefabs[spawnIndex];
            DoSpawnEnemy(enemyPrefab);
        }
    }

    private void DoSpawnEnemy(EnemySetup enemyPrefab)
    {
        PoolableObject poolableObject = enemyObjectPools[enemyPrefab.GetInstanceID()].GetObject();

        if (poolableObject != null)
        {
            EnemySetup enemy = poolableObject.GetComponent<EnemySetup>();
            int vertexIndex = Random.Range(0, triangulation.vertices.Length);

            // Try to place the enemy on the NavMesh
            if (NavMesh.SamplePosition(triangulation.vertices[vertexIndex], out NavMeshHit hit, 2f, -1))
            {
                enemy.agent.Warp(hit.position);
                Instantiate(spawnEffect, hit.position, Quaternion.identity);
                enemy.movement.target = player;
                enemy.agent.enabled = true;
                enemy.movement.StartChasing();
            }
            else
            {
                Debug.LogError($"Unable to place NavMeshAgent on NavMesh. Tried to use {triangulation.vertices[vertexIndex]}");
            }
        }
        else
        {
            Debug.LogError($"Unable to fetch enemy prefab {enemyPrefab.name} from object pool. Out of objects?");
        }
    }

    private void StartNextWave()
    {
        if (currentWaveIndex < waves.Length)
        {
            StartCoroutine(SpawnEnemies(waves[currentWaveIndex]));
            currentWaveIndex++;
        }
    }

    public enum SpawnMethod
    {
        RoundRobin,
        Random
    }
}
