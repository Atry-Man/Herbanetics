using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatEnemy : MonoBehaviour
{
    private EnemyClass batEnemy;
    [SerializeField] private int batHealth;
    [SerializeField] private float batSpeed;
    [SerializeField] private float batAttackDamage;
    [SerializeField] private float batAttackSpeed;

    // Start is called before the first frame update
    void Start()
    {
       batEnemy = new EnemyClass();
        batEnemy.name = "Bat Enemy";
        batEnemy.health = batHealth;
        batEnemy.speed = batSpeed; 
        batEnemy.attackDamage = batAttackDamage;  
        batEnemy.attackSpeed = batAttackSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
