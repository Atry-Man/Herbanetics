using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] PlayerConfig playerConfig;
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
        healthSlider.value = ((float)currentHealth / maxHealth);
        //Instantiate(hitEffect, hitSpawn.position, quaternion.identity);
        //playerAnim.SetTrigger(hurtStr);

        if (currentHealth <= 0)
        {
            Die();
        }
    }


    public void IncreaseHealth(int amount)
    {
        currentHealth += amount;
        healthSlider.value = currentHealth;
        if (currentHealth>= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    private void Die()
    {  
      gameOverPanel.SetActive(true);
      Time.timeScale = 0f;
      Destroy(gameObject);
    }
}
