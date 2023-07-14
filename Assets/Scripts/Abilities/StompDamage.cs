using UnityEngine;

public class StompDamage : MonoBehaviour
{
    [SerializeField] int stompDamage;
    
    private void OnTriggerEnter(Collider other)
    {   
        if (other.TryGetComponent<IDamagable>(out var damagable))
        {
            damagable.TakeDamage(stompDamage);
        }
    }
}
