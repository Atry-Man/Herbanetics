using UnityEngine;

public class Bolt : MonoBehaviour
{

    [SerializeField] SmolBoltsSO SmolBoltsSO;
    private const string obstacleStr = "Obstacle";
    private const string bossBarrierStr = "BossBarrier";
    private void OnTriggerEnter(Collider other)
    {  
        if(other.TryGetComponent<IDamagable>(out var damagable))
        {
            damagable.TakeDamage(SmolBoltsSO.boltDamage);
            Destroy(gameObject);
        }

        if(other.gameObject.CompareTag(obstacleStr) || other.gameObject.CompareTag(bossBarrierStr))
        {
            Destroy(gameObject);
        }
    }
}
