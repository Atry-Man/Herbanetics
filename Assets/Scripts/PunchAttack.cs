using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchAttack : MonoBehaviour
{
    [SerializeField] int maxCollisions;
    [SerializeField] int punchDamage;
    

    private void OnTriggerEnter(Collider other)
    {  
        if(other.gameObject.CompareTag("Enemy"))
        {
            EnemyStats enemyStats = other.gameObject.GetComponent<EnemyStats>();
            enemyStats.TakeDamage(punchDamage);
            
        }
    }

    

}
