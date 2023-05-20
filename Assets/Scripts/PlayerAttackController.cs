using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackController : MonoBehaviour
{
    [Header("Attack Combo Variables")]
    private int comboCount;
    private float lastAttackTime;
    [SerializeField] private float comboTimer;
    [SerializeField] string[] comboAttack;
    private bool previousAttack;
    [SerializeField] Animator playerAnim;
    [SerializeField] float DefaultimeBeforeMovement;
    [SerializeField] float lastAttackDelay;


    [Header("External Variables")]
    [SerializeField] PlayerController playerController;
    [SerializeField] BoxCollider[] ComboColliders; 
   


    private const string LeftPunchTrigger = "LeftPunch";
    private void Awake()
    {
        ResetCombo();
    }


    public void OnTripleWhammy(InputAction.CallbackContext context)
    {

        if(context.performed)
        {
            if(Time.time - lastAttackTime > comboTimer) {

                ResetCombo();
               
            }

            if (comboCount < comboAttack.Length)
            {
                if (comboCount == comboAttack.Length - 1)
                {
                    DefaultimeBeforeMovement = lastAttackDelay;

                }
                playerAnim.SetTrigger(comboAttack[comboCount]);

                if (comboCount != comboAttack.Length - 1)
                {
                    ComboColliders[comboCount].enabled = true;
                }
                playerController.CanMove = false;
                playerController.StopMovement();
                comboCount++;
                lastAttackTime = Time.time;
            }
        }
        else
        {
            StartCoroutine(DiableCollidersAndResumeMovement()); 
        }
    }


    void ResetCombo()
    {
        comboCount = 0;
        lastAttackTime = 0;
        DefaultimeBeforeMovement =  1.25f;
       

    }
    
    IEnumerator DiableCollidersAndResumeMovement()
    {
        yield return new WaitForSeconds(DefaultimeBeforeMovement);
        foreach(Collider col  in ComboColliders)
            {
                col.enabled = false;
            }
        playerController.CanMove = true;
    }

    
}
