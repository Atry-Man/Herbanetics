using UnityEngine;
using UnityEngine.InputSystem;

public class SmolBolts : MonoBehaviour
{
    [Header("Smol Bolts references")]
    [SerializeField] Transform firePoint;
    [SerializeField] Transform firePoint2;
    [SerializeField] Transform firePoint3;
    [SerializeField] float maxDistance;
    [SerializeField] Animator playerAnim;
    [SerializeField] SmolBoltsSO SmolBoltsSO;
    private float fireRateTimer;
    private bool canShoot;
    private const string isShootingStr = "isShooting";
    [SerializeField] GameObject reticle;

    private void Awake()
    {
        canShoot = true;
    }


  
    public void ShootBolts(InputAction.CallbackContext ctx)
    {
        if (ctx.action.triggered && canShoot)
        {
            playerAnim.SetBool(isShootingStr, true);

            //ProjectileSpawner();

            fireRateTimer = 0f;
            canShoot = false;
        }
        else
        {
            playerAnim.SetBool(isShootingStr, false);
        }
    }

    public void ProjectileSpawner()
    {
        
        int numBolts = SkillManager.instance.projectileSkillLevel + 1;

        if (SkillManager.instance.projectileSkillLevel >= 2)
            numBolts = 3;


            for (int i = 0; i < numBolts; i++)
        {
            switch (i)
            {
                case 0:
                    SpawnBolt(firePoint);
                    break;
                case 1:
                    SpawnBolt(firePoint2);
                    break;
                case 2:
                    SpawnBolt(firePoint3);
                    break;

            }
        }
    }


    private bool FindClosestEnemy(Vector3 position, out Transform target)
    {
        target = null;
        float closestDistance = maxDistance;

        // Find all enemies in the scene (you might want to change this to a more efficient method)
        EnemySetup[] enemies = FindObjectsOfType<EnemySetup>();

        foreach (EnemySetup enemy in enemies)
        {
            float distance = Vector3.Distance(position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                target = enemy.transform;
            }
        }

        return target != null;
    }

    private void SpawnBolt(Transform firePoint)
    {
        GameObject bolt = Instantiate(SmolBoltsSO.boltPrefab, firePoint.position, firePoint.rotation);
        if (FindClosestEnemy(firePoint.position, out Transform targetEnemy))
        {
            Vector3 directionToTarget = (targetEnemy.position - bolt.transform.position).normalized;
            reticle.transform.position = targetEnemy.transform.position + directionToTarget;
            bolt.GetComponent<Rigidbody>().velocity = directionToTarget * SmolBoltsSO.fireForce;
        }
        else
        {
            // No enemy in range, use the original behavior
            bolt.GetComponent<Rigidbody>().AddForce(firePoint.forward * SmolBoltsSO.fireForce, ForceMode.Impulse);
        }
    }




    private void Update()
    {
        fireRateTimer += Time.deltaTime;

        if(fireRateTimer > SmolBoltsSO.fireRate) {
               canShoot = true;
        }
    }

}
