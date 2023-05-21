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
    [SerializeField] GameObject[] waveText;

    private int currentWaveIndex;
    private int enemiesRemaining;
    private float waveTimer;
    [SerializeField] float startDelay;
    [SerializeField] PlayerStats playerStats;
    
    void Start()
    {
        StartCoroutine(SpawnWaves(startDelay));
    }

   
    IEnumerator SpawnWaves(float startDelay)
    {    
        yield return new WaitForSeconds(startDelay); 

        while(currentWaveIndex < totalWaves)
        {

            Wave currentWave = waves[currentWaveIndex];
            waveText[currentWaveIndex].SetActive(true);

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

            playerStats.IncreaseHealth(4);

            yield return new WaitForSeconds(timeBtnWaves);
        }

        waveText[totalWaves].SetActive(true);
        Debug.Log("All Waves completed");
    }

    public void EnemyDefeated()
    {
        enemiesRemaining--;
    }
}
