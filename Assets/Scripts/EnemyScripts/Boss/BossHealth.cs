using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour,IDamagable
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] int healthThreshold = 50;
    [SerializeField] BossWaveAttacks waveAttacks;
    public bool isBossDefeated;
    [SerializeField] GameObject bossDamageEffect;
    [SerializeField] Transform bossHitSpawn;
    [SerializeField] Image bossHealthSlider;
    [SerializeField] Animator bossAnim;
    private int currentHealth;
    public bool isInSecondPhase;

    [Header("Second Phase Variables")]
    [SerializeField] float newWaveForce;
    [SerializeField] float newTimeForWarnings;
    private const string deathStr = "Death";
    private void Start()
    {
        currentHealth = maxHealth;
        bossHealthSlider.fillAmount = currentHealth;
        isInSecondPhase = false;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Instantiate(bossDamageEffect, bossHitSpawn.position, Quaternion.identity);
        bossHealthSlider.fillAmount -= (float)damage/maxHealth;

       
        if (currentHealth <= healthThreshold)
        {
            isInSecondPhase = true;
            
            waveAttacks.attackWaveForce = newWaveForce;
            waveAttacks.warningDelay = newTimeForWarnings;

        }  
        if (currentHealth <= 0)
        {    isBossDefeated = true;
            bossAnim.SetTrigger(deathStr);
            Destroy(gameObject);
        }
    }

    public Transform GetTransform()
    {
       return transform;
    }
}
