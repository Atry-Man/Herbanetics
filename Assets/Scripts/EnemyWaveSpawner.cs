using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    [SerializeField] float startDelay;
    [SerializeField] PlayerDamage playerDamage;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject[] waveText;
    [SerializeField] GameObject enemySpawnEffect;
    private int currentWaveIndex;
    private int enemiesRemaining;
    private float waveTimer;
    EnemyMovement enemyMovement;
    [SerializeField] UnityEvent levelCompleteEvent;
    [SerializeField] float healthIncreasePercentage;
    private void OnEnable()
    {
        EnemySetup.EnemyDestroyed += EnemyDefeated;
    }

    private void OnDisable()
    {
        EnemySetup.EnemyDestroyed -= EnemyDefeated;
    }
    void Start()
    {
        StartCoroutine(SpawnWaves(startDelay));
    }

    IEnumerator SpawnWaves(float startDelay)
    {    
        yield return new WaitForSeconds(startDelay);
        
        while (currentWaveIndex < totalWaves)
        {

            Wave currentWave = waves[currentWaveIndex];
            waveText[currentWaveIndex].SetActive(true);

            for (int i = 0; i < currentWave.enemyPrefabs.Count; i++)
            {

                GameObject enemyPrefab = currentWave.enemyPrefabs[i];
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Instantiate(enemySpawnEffect, spawnPoint.position, enemySpawnEffect.transform.rotation);
                yield return new WaitForSeconds(0.5f);

                GameObject newEnemy =  Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
                newEnemy.SetActive(true);
                enemyMovement = newEnemy.GetComponent<EnemyMovement>();
                enemyMovement.StartChasing();
                enemiesRemaining++;


                yield return new WaitForSeconds(currentWave.spawnInterval);

            }

            while (enemiesRemaining > 0)
            {

                yield return null;
            }


            currentWaveIndex++;
            waveTimer = timeBtnWaves;
            playerDamage.IncreaseHealth(healthIncreasePercentage);
            //Debug.Log("Wave " + currentWave.name + "Completed. Proceeding to the next wave");

            yield return new WaitForSeconds(timeBtnWaves);
        }

       
        waveText[totalWaves].SetActive(true);
        yield return new WaitForSeconds(1.5f);
        levelCompleteEvent?.Invoke();
    }

    public void EnemyDefeated()
    {
        enemiesRemaining--;
    }
}
