using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    EnemyWaveSpawner waveSpawner;

    private void Awake()
    {
        waveSpawner = GameObject.FindWithTag("Spawner").GetComponent<EnemyWaveSpawner>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {   
            Destroy(collision.gameObject);
            waveSpawner.EnemyDefeated();
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
