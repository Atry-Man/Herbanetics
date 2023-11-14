using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TokolosheController : MonoBehaviour,IDamagable
{
    [Header("Boss Variables")]
    [SerializeField] int maxHealth = 100;
    [SerializeField] int healthThreshold = 50;
    public bool isBossDefeated;
    [SerializeField] Image bossHealthSlider;
    [SerializeField] GameObject bossDamageEffect;
    [SerializeField] Transform bossHitSpawn;
    Animator bossAnim;
    [SerializeField] UnityEvent levelCompleteEvent;
    private int currentHealth;
    public bool isInSecondPhase;
    private const string Death = "Death";
    private const string isMoving = "Walk";
    private const string isAttacking = "Attack";
    private void Awake()
    {
        bossAnim = GetComponent<Animator>();
        currentHealth = maxHealth;
        bossHealthSlider.fillAmount = currentHealth;
    }
    private void Start()
    {
       
        isInSecondPhase = false;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Instantiate(bossDamageEffect, bossHitSpawn.position, Quaternion.identity);
        bossHealthSlider.fillAmount -= (float)damage / maxHealth;

        if (currentHealth <= healthThreshold)
        {
            isInSecondPhase = true;

           

        }
        if (currentHealth <= 0)
        {
            isBossDefeated = true;
            bossAnim.SetTrigger(Death);
            bossAnim.SetBool(isMoving, false);
            bossAnim.SetBool(isAttacking, false);
        }
    }
    

    public Transform GetTransform()
    {
        return transform;
    }

    public void BossCompletedUI()
    {
        levelCompleteEvent?.Invoke();
    }
}
