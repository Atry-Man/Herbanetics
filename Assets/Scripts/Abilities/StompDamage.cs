using UnityEngine;

public class StompDamage : MonoBehaviour
{
    [SerializeField] StompFrontSO stompFrontSO;
    
    private void OnTriggerEnter(Collider other)
    {   
        if (other.TryGetComponent<IDamagable>(out var damagable))
        {
            damagable.TakeDamage(stompFrontSO.stompDamage);
        }
    }
}
