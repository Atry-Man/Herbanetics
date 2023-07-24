using UnityEngine;

public class PunchAttack : MonoBehaviour
{
    [SerializeField] BigPunchSO bigPunchSO;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamagable>(out var damagable))
        {
            damagable.TakeDamage(bigPunchSO.bigPunchDamage);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }



}
