using System.Collections;
using UnityEngine;

public class BossFightHandler : MonoBehaviour
{
    [SerializeField] BossController bossController;
    [SerializeField] BossWaveAttacks bossWaveAttacks;
    [SerializeField] BossVulnerablePhase bossVulnerablePhase;
    [SerializeField] BossHealth bossHealth;
    Coroutine bossRoutine;
    private void OnEnable()
    {
        bossController.StartFight += StartBoss;
    }

    private void OnDisable()
    {
        bossController.StartFight -= StartBoss;
    }

    void StartBoss()
    {
        // Start the boss fight when the game level starts
        bossRoutine ??= StartCoroutine(BossFightRoutine());
        
    }

    private IEnumerator BossFightRoutine()
    {
        while (!bossHealth.isBossDefeated)
        {    
            // Boss spawns warning indicators and starts wave attacks
            yield return StartCoroutine(bossWaveAttacks.BossWaveAttackRoutine());

            // Boss becomes vulnerable and player can attack
            yield return StartCoroutine(bossVulnerablePhase.BossVulnerableRoutine());

            if (bossHealth.isBossDefeated)
            {
                // Boss is defeated, add victory behavior or end the game here...
                break;
            }
            else
            {
               StopCoroutine(bossRoutine);
               bossRoutine = null;

               bossRoutine ??= StartCoroutine(BossFightRoutine());
               
            }
            
        }
    }

}
