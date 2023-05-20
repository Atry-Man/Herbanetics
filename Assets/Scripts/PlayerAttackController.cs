using System.Collections;
using System.Collections.Generic;
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

            if(!previousAttack)
            {   
               
                playerAnim.SetTrigger(LeftPunchTrigger);
                playerController.CanMove = false;
                playerController.StopMovement();
                previousAttack = true;
            }
            else
            {
                if(comboCount < comboAttack.Length)
                {   
                    if(comboCount == comboAttack.Length - 1)
                    {
                        DefaultimeBeforeMovement = lastAttackDelay;
                    }
                    playerAnim.SetTrigger(comboAttack[comboCount]);
                    playerController.CanMove = false;
                    playerController.StopMovement();
                    comboCount++;
                    lastAttackTime = Time.time;
                }
            }
        }
        else
        {
            StartCoroutine(ResumeMovement());
            
            
        }
    }


    void ResetCombo()
    {
        comboCount = 0;
        lastAttackTime = 0;
        DefaultimeBeforeMovement =  1.25f;
    }

    IEnumerator ResumeMovement()
    {
        yield return new WaitForSeconds(DefaultimeBeforeMovement);
        playerController.CanMove = true;
    }
}
