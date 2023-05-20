using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int damage;
    EnemyWaveSpawner waveSpawner;
    [SerializeField] GameObject deathImpact;
    private int currentHealth;

    public int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public int CurrentHealth
    {
        get { return currentHealth; }
    }

    private void Awake()
    {
        waveSpawner = GameObject.FindWithTag("Spawner").GetComponent<EnemyWaveSpawner>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
           
        }
    }

    private void Die()
    {
        Instantiate(deathImpact, transform.position, transform.rotation);
        waveSpawner.EnemyDefeated();
        Destroy(gameObject);
    }
}
