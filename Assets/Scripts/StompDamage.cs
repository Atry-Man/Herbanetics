using UnityEngine;

public class StompDamage : MonoBehaviour
{
    EnemyWaveSpawner waveSpawner;

    private void Awake()
    {
        waveSpawner = GameObject.FindWithTag("Spawner").GetComponent<EnemyWaveSpawner>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            waveSpawner.EnemyDefeated();
        }
    }
}
