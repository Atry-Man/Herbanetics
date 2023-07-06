using UnityEngine;

public class Bolt : MonoBehaviour
{
   
    [SerializeField] int boltDamage;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            EnemyStats enemyStats = other.gameObject.GetComponent<EnemyStats>();
            enemyStats.TakeDamage(boltDamage);
            Destroy(gameObject);
        }

        if(other.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
