using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchAttack : MonoBehaviour
{
    [SerializeField] int punchDamage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamagable>(out var damagable))
        {
            damagable.TakeDamage(punchDamage);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }



}
