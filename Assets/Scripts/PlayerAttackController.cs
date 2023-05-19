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
                //play animation
                playerAnim.SetTrigger(LeftPunchTrigger);
                previousAttack = true;
            }
            else
            {
                if(comboCount < comboAttack.Length)
                {
                    //Play animations from string list
                    playerAnim.SetTrigger(comboAttack[comboCount]);
                    comboCount++;
                    lastAttackTime = Time.time;
                }
            }
        }
    }


    void ResetCombo()
    {
        comboCount = 0;
        lastAttackTime = 0;
    }
}
