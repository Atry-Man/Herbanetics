using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackController : MonoBehaviour
{
    [Header("Attack Combo Variables")]
    [SerializeField] Animator playerAnim;
    private const string punchStr = "Punch";

    [SerializeField] BigPunchSO bigPunchSO;
    [SerializeField] Transform punchPos;
    [SerializeField] Transform punchPos2;
    [SerializeField] Transform punchPos3;


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
            SpawnPunch(punchPosition);
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

    private void SpawnPunch(Transform punchPosition)
    {
        GameObject punch = Instantiate(bigPunchSO.punchPrefab, punchPosition.position, Quaternion.identity);
        punch.GetComponent<Rigidbody>().AddForce(punchPosition.forward * bigPunchSO.fireForce, ForceMode.Impulse);
        Vector3 movDir = punchPosition.forward.normalized;
        punch.transform.rotation = Quaternion.LookRotation(movDir);
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
