using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class EnemyWaveSpawner : EnemyWaveBasic
{
    [SerializeField] PlayerDamage playerDamage;

    [Header("Enemy Wave Settings")]

    private int currentWaveIndex;
    private int enemiesRemaining;
    private float waveTimer;
    EnemyMovement enemyMovement;
    [SerializeField] UnityEvent levelCompleteEvent;
    [SerializeField] float healthIncreasePercentage;

    [Header("Wave Text info")]
    [SerializeField] private TextMeshProUGUI waveTextInfo;
    [SerializeField] private GameObject completeTitle;
    [Header("Sun and moon")]
    [SerializeField] private GameObject[] skyTime;
    

    private void OnEnable()
    {
        EnemySetup.EnemyDestroyed += EnemyDefeated;
    }

    private void OnDisable()
    {
        EnemySetup.EnemyDestroyed -= EnemyDefeated;
    }



    protected override void Start()
    {
        StartCoroutine(SpawnWaves(startDelay));
        waveTextInfo.text = "WAVES" + " " + (currentWaveIndex + 1).ToString() + " " + "/ 5";
    }



    protected override void Update()
    {
        base.Update();
        if(currentWaveIndex == totalWaves && enemiesRemaining <= 0)
        {
            completeTitle.SetActive(true);
        }
        SwitchTimeZone(currentWaveIndex);
    }

    IEnumerator SpawnWaves(float startDelay)
    {
        yield return new WaitForSeconds(startDelay);

        while (currentWaveIndex < totalWaves)
        {

            Wave currentWave = waves[currentWaveIndex];
            waveTextInfo.gameObject.SetActive(true);
            waveTextInfo.text = "WAVES" + " " + (currentWaveIndex + 1).ToString()  + " " + "/" + " " + totalWaves.ToString();

            for (int i = 0; i < currentWave.enemyPrefabs.Count; i++)
            {

                GameObject enemyPrefab = currentWave.enemyPrefabs[i];
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Instantiate(enemySpawnEffect, spawnPoint.position, enemySpawnEffect.transform.rotation);
                yield return new WaitForSeconds(0.05f);

                SpawnEnemy(enemyPrefab, spawnPoint);

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


        //waveText[totalWaves].SetActive(true);
        yield return new WaitForSeconds(1.5f);
        levelCompleteEvent?.Invoke();
    }

    private void SpawnEnemy(GameObject enemyPrefab, Transform spawnPoint)
    {
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        newEnemy.SetActive(true);
        enemyMovement = newEnemy.GetComponent<EnemyMovement>();
        enemyMovement.StartChasing();
        enemiesRemaining++;
    }

    public void EnemyDefeated()
    {
        enemiesRemaining--;
    }

    private void SwitchTimeZone(int _index)
    {
        switch (_index)
        {
            case 0:
                Debug.Log("test log 1");
                TimeMovement(skyTime[_index], null);
                break;
            case 1:
                Debug.Log("test log 2");
                TimeMovement(skyTime[_index], skyTime[_index - 1]);
                break;
            default:
                Debug.Log("default");
                break;
        }
    }

    private void TimeMovement( GameObject _sunOrMoon, GameObject _oldSky)
    {
        if(_oldSky != null)
        _oldSky.SetActive(false);


        _sunOrMoon.SetActive(true);
    }
}

