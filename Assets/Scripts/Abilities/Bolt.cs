using UnityEngine;

public class Bolt : MonoBehaviour
{
   
    [SerializeField] int boltDamage;

    private void OnTriggerEnter(Collider other)
    {  
        if(other.TryGetComponent<IDamagable>(out var damagable))
        {
            damagable.TakeDamage(boltDamage);
            Destroy(gameObject);
        }

        if(other.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
