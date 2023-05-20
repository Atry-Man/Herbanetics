using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string name;
        public List<GameObject> enemyPrefabs;
        public float spawnInterval;
    }

    [Header("Enemy Wave Settings")]
    [SerializeField] List<Wave> waves;
    [SerializeField] int totalWaves;
    [SerializeField] float timeBtnWaves;
    [SerializeField] Transform[] spawnPoints;

    private int currentWaveIndex;
    private int enemiesRemaining;
    private float waveTimer;
    
    void Start()
    {
        
    }

   
    IEnumerator SpawnWaves()
    {
        while(currentWaveIndex < totalWaves)
        {

            Wave currentWave = waves[currentWaveIndex];
            Debug.Log("Starting Wave "+ currentWave.name);

            for(int i = 0; i< currentWave.enemyPrefabs.Count; i++) { 
                
                GameObject enemyPrefab = currentWave.enemyPrefabs[i];
                Transform spawnPoint = spawnPoints[i % spawnPoints.Length];
                Instantiate(enemyPrefab, spawnPoint.position,spawnPoint.rotation);
                enemiesRemaining++;

                yield return new WaitForSeconds(currentWave.spawnInterval);
            
            }

            while(enemiesRemaining > 0)
            {
                yield return null;
            }


            currentWaveIndex++;
            waveTimer = timeBtnWaves;

            Debug.Log("Wave " + currentWave.name + "Completed. Proceeding to the next wave");

            yield return new WaitForSeconds(timeBtnWaves);
        }

        Debug.Log("All Waves completed");
    }

    public void EnemyDefeated()
    {
        enemiesRemaining--;
    }
}
