using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWaveSpawner : MonoBehaviour
{
    [SerializeField] Transform player;  // Reference to the player's transform
    [SerializeField] int numberOfEnemiesToSpawn;  // Number of enemies to spawn
    [SerializeField] float spawnDelay;  // Delay between enemy spawns
    [SerializeField] List<EnemySetup> enemyPrefabs = new();  // List of enemy prefabs to spawn
    public SpawnMethod enemySpawnMethod = SpawnMethod.RoundRobin;  // Method for spawning enemies
    [SerializeField] GameObject spawnEffect;
    private NavMeshTriangulation triangulation;  // NavMesh triangulation data
    private Dictionary<int, ObjectPool> EnemyObjectPools = new Dictionary<int, ObjectPool>();  // Dictionary of enemy object pools


    private void Awake()
    {
        // Create object pools for each enemy prefab
        for (int i = 0; i < enemyPrefabs.Count; i++)
        {
            EnemyObjectPools.Add(i, ObjectPool.CreateInstance(enemyPrefabs[i], numberOfEnemiesToSpawn));
        }
    }

    private void Start()
    {
        triangulation = NavMesh.CalculateTriangulation();  // Calculate NavMesh triangulation data
        StartCoroutine(SpawnEnemies());  // Start spawning enemies
    }

    private IEnumerator SpawnEnemies()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnDelay);

        int spawnedEnemies = 0;

        while (spawnedEnemies < numberOfEnemiesToSpawn)
        {
            if (enemySpawnMethod == SpawnMethod.RoundRobin)
            {
                SpawnRoundRobinEnemy(spawnedEnemies);
            }
            else if (enemySpawnMethod == SpawnMethod.Random)
            {
                SpawnRandomEnemy();
            }
            spawnedEnemies++;

            yield return wait;
        }
    }

    private void SpawnRoundRobinEnemy(int spawnedEnemies)
    {
        int spawnIndex = spawnedEnemies % enemyPrefabs.Count;
        DoSpawnEnemy(spawnIndex);
    }

    private void SpawnRandomEnemy()
    {
        DoSpawnEnemy(Random.Range(0, enemyPrefabs.Count));
    }

    private void DoSpawnEnemy(int spawnIndex)
    {

        PoolableObject poolableObject = EnemyObjectPools[spawnIndex].GetObject();

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
            Debug.LogError($"Unable to fetch enemy of type {spawnIndex} from object pool. Out of objects?");
        }
    }



    public enum SpawnMethod
    {
        RoundRobin,
        Random
    }
}
