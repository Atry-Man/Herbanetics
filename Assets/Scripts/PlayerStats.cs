using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] Slider healthSlider;
    private int currentHealth;
   

    public int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    public int CurrentHealth
    {
        get { return currentHealth; }
    }

    private void Start()
    {
        currentHealth = maxHealth;
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        healthSlider.value = currentHealth;
        if (currentHealth <= 0)
        {
            Die();

        }
    }

    public void IncreaseHealth(int amount)
    {
        currentHealth += amount;
        healthSlider.value = currentHealth;
        if (currentHealth>= MaxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    private void Die()
    {  
      Destroy(gameObject);
    }
}
