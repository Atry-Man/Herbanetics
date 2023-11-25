using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class PlayerDamage : MonoBehaviour,IDamagable
{
    [SerializeField] Slider healthSlider;
    [SerializeField] PlayerConfig playerConfig;
    [SerializeField] GameObject hitEffect;
    [SerializeField] Transform hitSpawn;
    [SerializeField] Animator playerAnim;
    InputDection inputDection;
   
    public static event Action StopAttacking;
    public static event Action ScoreUpdate;
    private int maxHealth;
    private int currentHealth;
    private const string deathStr = "Death";
    [SerializeField] UnityEvent gameOverUI;
   
    private bool canTakeDamage;
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
        canTakeDamage = true;
        inputDection = GameObject.FindObjectOfType<InputDection>();
    }

    public void TakeDamage(int amount)
    {   
        if(canTakeDamage)
        {
            currentHealth -= amount;
            healthSlider.value = currentHealth;
            Instantiate(hitEffect, hitSpawn.position, Quaternion.identity);

            if (currentHealth <= 0)
            {
                Die();
            }
        }
       
    }

   public void IncreaseHealth(float healthIncreasePercent)
    {   
        float increase = (healthIncreasePercent / 100) * (float)maxHealth;
        int calculatedHealth = (int)increase;
        if (currentHealth <= maxHealth)
        {
            currentHealth += calculatedHealth;  
            healthSlider.value = currentHealth;
        }
    }
    private void Die()
    {
      canTakeDamage = false;
      inputDection.CanUseControls = false;
      playerAnim.SetTrigger(deathStr);
      StopAttacking?.Invoke();
     
    }

    public void DeathScreenStuff()
    {
       gameOverUI.Invoke();
       Time.timeScale = 0f;
       ScoreUpdate?.Invoke();
       gameObject.SetActive(false);
    }
    public Transform GetTransform()
    {
        return transform;
    }
}
