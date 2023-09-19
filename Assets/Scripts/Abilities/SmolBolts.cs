using UnityEngine;
using UnityEngine.InputSystem;

public class SmolBolts : MonoBehaviour
{
    [Header("Smol Bolts references")]
    [SerializeField] Transform firePoint;
    [SerializeField] Animator playerAnim;
    [SerializeField] SmolBoltsSO SmolBoltsSO;
    private float fireRateTimer;
    private bool canShoot;
    private const string isShootingStr = "isShooting";
    [SerializeField] Transform playerPos;
    Vector3 shootingDirection;

    private void Awake()
    {
        canShoot = true;
    }
    public void ShootBolts(InputAction.CallbackContext ctx)
    {
        if (ctx.action.triggered && canShoot)
        {
            playerAnim.SetBool(isShootingStr, true);

           

            if (ctx.control.device is Mouse)
            {
                Vector3 mousePos;
                Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                if(Physics.Raycast(ray, out RaycastHit hitInfo))
                {
                    mousePos = new Vector3( hitInfo.point.x, 0, hitInfo.point.z);
                    Debug.DrawLine(playerPos.position, mousePos);
                    shootingDirection = (mousePos - playerPos.position).normalized;
                } 
                
            }
            else
            {
               
                shootingDirection = transform.forward;
            }

            GameObject bolt = Instantiate(SmolBoltsSO.boltPrefab, firePoint.position, Quaternion.identity);

            
            bolt.transform.forward = shootingDirection;

            bolt.GetComponent<Rigidbody>().AddForce(shootingDirection * SmolBoltsSO.fireForce, ForceMode.Impulse);
            fireRateTimer = 0f;
            canShoot = false;
        }
        else
        {
            playerAnim.SetBool(isShootingStr, false);
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
