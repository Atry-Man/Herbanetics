using UnityEngine;
using System;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour,IDamagable
{
    [SerializeField] Slider healthSlider;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] PlayerConfig playerConfig;
    public static event Action StopAttacking;
    private int maxHealth;
    private int currentHealth;

    public int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    public int CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }

    private void Awake()
    {
        maxHealth = playerConfig.maxHealth;
        currentHealth = maxHealth;
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        healthSlider.value = currentHealth;
        //Instantiate(hitEffect, hitSpawn.position, quaternion.identity);
        //playerAnim.SetTrigger(hurtStr);

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {  
      gameOverPanel.SetActive(true);
      StopAttacking?.Invoke();
      Time.timeScale = 0f;
      Destroy(gameObject);
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
