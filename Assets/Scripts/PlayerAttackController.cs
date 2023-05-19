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
    //[SerializeField] Animator animator;

    private void Awake()
    {
        
    }

    void OnTripleWhammy(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            if(Time.time - lastAttackTime > comboTimer) {

                ResetCombo();
                
            }

            if(!previousAttack)
            {   
                //play animation

                previousAttack = true;
            }
            else
            {
                if(comboCount < comboAttack.Length)
                {
                    //Play animations from string list
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
