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

   
    void Update()
    {
        
    }
}
