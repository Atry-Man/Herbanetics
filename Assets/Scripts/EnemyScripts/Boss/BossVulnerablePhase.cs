using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossVulnerablePhase : MonoBehaviour
{
    [SerializeField] GameObject bossObject;
    [SerializeField] float vulnerablePhaseDuration = 10f;

    public IEnumerator BossVulnerableRoutine()
    {
        yield return new WaitForSeconds(2f);
        // Enable the boss to make it vulnerable
        bossObject.SetActive(true);
        yield return new WaitForSeconds(vulnerablePhaseDuration);

        // Disable the boss to end the vulnerable phase
        bossObject.SetActive(false);    
    }
}
