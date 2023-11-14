using UnityEngine;

public class StompDamage : MonoBehaviour
{
    [SerializeField] StompFrontSO stompFrontSO;
    private const string playerStr = "Player";
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.TryGetComponent<IDamagable>(out var damagable) && !other.gameObject.CompareTag(playerStr))
        {
            //damagable.TakeDamage(stompFrontSO.stompDamage);
            other.GetComponent<StatusEffectManager>().ApplyStompTick(stompFrontSO.stompNumberOfTicks);
        }
    }
}
