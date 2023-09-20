using UnityEngine;
using UnityEngine.InputSystem;

public class SmolBolts : MonoBehaviour
{
    [Header("Smol Bolts references")]
    [SerializeField] Transform firePoint;
    [SerializeField] Transform firePoint2;
    [SerializeField] Transform firePoint3;

    [SerializeField] Animator playerAnim;
    [SerializeField] SmolBoltsSO SmolBoltsSO;
    private float fireRateTimer;
    private bool canShoot;
    private const string isShootingStr = "isShooting";
   

    private void Awake()
    {
        canShoot = true;
    }
    public void ShootBolts(InputAction.CallbackContext ctx)
    {
        if (ctx.action.triggered && canShoot)
        {
            playerAnim.SetBool(isShootingStr, true);

            int numBolts = SkillManager.instance.projectileSkillLevel + 1;

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

            fireRateTimer = 0f;
            canShoot = false;
        }
        else
        {
            playerAnim.SetBool(isShootingStr, false);
        }
    }

   

    private void SpawnBolt(Transform firePoint)
    {
        GameObject bolt = Instantiate(SmolBoltsSO.boltPrefab, firePoint.position, firePoint.rotation);
        bolt.GetComponent<Rigidbody>().AddForce(firePoint.forward * SmolBoltsSO.fireForce, ForceMode.Impulse);
    }
    private void Update()
    {
        fireRateTimer += Time.deltaTime;

        if(fireRateTimer > SmolBoltsSO.fireRate) {
               canShoot = true;
        }
    }

}
