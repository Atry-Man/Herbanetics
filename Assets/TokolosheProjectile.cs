using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokolosheProjectile : MonoBehaviour
{
    [SerializeField] float explosionRadius;
    [SerializeField] int explosionDamage;
    [SerializeField] GameObject explosionEffectPrefab;
    private const string bossStr = "Boss";

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag(bossStr)){

            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);


            Explode();


            Destroy(gameObject);
        }
           
        
        
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider collider in colliders)
        {
            IDamagable damagable = collider.GetComponent<IDamagable>();
            damagable?.TakeDamage(explosionDamage);
        }
    }
}
