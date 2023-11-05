using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.EventSystems.EventTrigger;

public class PlayerAttackController : MonoBehaviour
{
    [Header("Attack Combo Variables")]
    [SerializeField] Animator playerAnim;
    private const string punchStr = "Punch";

    [SerializeField] BigPunchSO bigPunchSO;
    [SerializeField] Transform punchPos;
    [SerializeField] Transform punchPos2;
    [SerializeField] Transform punchPos3;
    [SerializeField] float maxDistance;
    [SerializeField] GameObject reticle;
    public static Action StartPunchActiveCooldown;
    private bool canPunch;
    private float fireRateTimer;

    private void Awake()
    {
        canPunch = true;
    }
    public void OnBigPunch(InputAction.CallbackContext context)
    {
        if (context.performed && canPunch)
        {
            playerAnim.SetTrigger(punchStr);

          

            fireRateTimer = 0f;
            canPunch = false;
            //StartPunchActiveCooldown?.Invoke();
        }
    }

    public void Punch()
    {
        int numPunches = SkillManager.instance.punchSkillLevel + 1;

        if (SkillManager.instance.punchSkillLevel >= 2)
            numPunches = 3;

        for (int i = 0; i < numPunches; i++)
        {
            Transform punchPosition = GetPunchPosition(i);
            SpawnPunch(punchPosition, i);
        }
    }
    private Transform GetPunchPosition(int punchIndex)
    {
        switch (punchIndex)
        {
            case 0:
                return punchPos;
            case 1:
                return punchPos2;
            case 2:
                return punchPos3;
           
            default:
                return punchPos; 
        }
    }

    private void SpawnPunch(Transform punchPosition, int punchIndex)
    {
        GameObject punch;

        if(punchIndex == 0)
        {
             punch = Instantiate(bigPunchSO.punchPrefab, punchPosition.position, Quaternion.identity);

        }else if(punchIndex == 1)
        {
            punch = Instantiate(bigPunchSO.punchPrefab2, punchPosition.position, Quaternion.identity);
        }
        else if(punchIndex == 2)
        {
             punch = Instantiate(bigPunchSO.punchPrefab3, punchPosition.position, Quaternion.identity);
        }
        else
        {
            punch = Instantiate(bigPunchSO.punchPrefab, punchPosition.position, Quaternion.identity);
        }

       

        if (FindClosestEnemy(punchPosition.position, out Transform targetEnemy))
        {   
            reticle.SetActive(true);
            Vector3 directionToTarget = (targetEnemy.position - punch.transform.position).normalized;
            reticle.transform.position = targetEnemy.transform.position + directionToTarget;
            punch.GetComponent<Rigidbody>().velocity = directionToTarget * bigPunchSO.fireForce;
            punch.transform.rotation = Quaternion.LookRotation(directionToTarget);
            Invoke(nameof(TurnOffReticle), 0.5f);
        }
        else
        {
           
            punch.GetComponent<Rigidbody>().AddForce(punchPosition.forward * bigPunchSO.fireForce, ForceMode.Impulse);
        }

       
        Vector3 movDir = punchPosition.forward.normalized;
        punch.transform.rotation = Quaternion.LookRotation(movDir);
    }

    private bool FindClosestEnemy(Vector3 position, out Transform target)
    {
        target = null;
        float closestDistance = maxDistance;

       
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

    void TurnOffReticle()
    {
        reticle.SetActive(false);
    }

    private void Update()
    {
        fireRateTimer += Time.deltaTime;

        if (fireRateTimer > bigPunchSO.fireRate)
        {
            canPunch = true;
        }
    }
}
