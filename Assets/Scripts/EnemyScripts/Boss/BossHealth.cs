using UnityEngine;

public class BossHealth : MonoBehaviour,IDamagable
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] int healthThreshold = 50;
    [SerializeField] BossWaveAttacks waveAttacks;
    public bool isBossDefeated;

    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Check if the boss's health reaches the threshold
        if (currentHealth <= healthThreshold)
        {
            // Change boss behavior to spawn two waves at a faster interval
            waveAttacks.timeBetweenWaveAttacks = 1f;
        }

        // Check if the boss's health reaches zero
        if (currentHealth <= 0)
        {    isBossDefeated = true;
            // Boss defeated, add any additional behavior or end the game as needed
        }
    }

    public Transform GetTransform()
    {
       return transform;
    }
}
