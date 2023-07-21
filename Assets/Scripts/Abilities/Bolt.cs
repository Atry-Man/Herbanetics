using UnityEngine;

public class Bolt : MonoBehaviour
{

    [SerializeField] SmolBoltsSO SmolBoltsSO;

    private void OnTriggerEnter(Collider other)
    {  
        if(other.TryGetComponent<IDamagable>(out var damagable))
        {
            damagable.TakeDamage(SmolBoltsSO.boltDamage);
            Destroy(gameObject);
        }

        if(other.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
