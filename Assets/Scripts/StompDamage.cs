using UnityEngine;

public class StompDamage : MonoBehaviour
{
    [SerializeField] int stompDamage;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {    
            EnemyStats enemyStats = other.GetComponent<EnemyStats>();
            enemyStats.TakeDamage(stompDamage);
            
        }
    }
}
