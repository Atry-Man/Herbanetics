using System.Collections;
using UnityEngine;

public class BossWaveAttacks : MonoBehaviour
{
    [SerializeField] GameObject[] warningIndicators;
    [SerializeField] GameObject [] bossWaveAttacks;
    [SerializeField] Transform[] waveAttackSpots;
    public float timeBetweenWaveAttacks;
    [SerializeField] float attackWaveForce;
    [SerializeField] float numberOfAttacks;
    [SerializeField] Animator bossAnim;
    private const string attackStr = "Attack";

    public IEnumerator BossWaveAttackRoutine()
    {

       
        float tempNumOfAttacks = numberOfAttacks;

        while (tempNumOfAttacks > 0)
        {
            bossAnim.SetTrigger(attackStr);
            // Choose a random set of warning indicators and wave attack from the enemy wave spawner
            int randomWaveSetIndex = Random.Range(0, warningIndicators.Length);
            warningIndicators[randomWaveSetIndex].SetActive(true);
            yield return new WaitForSeconds(1f); // Add a short delay for warning visibility

            WaveAttack(randomWaveSetIndex);
            warningIndicators[randomWaveSetIndex].SetActive(false);

            // Wait for the wave attack to finish before spawning the next wave
            yield return new WaitForSeconds(timeBetweenWaveAttacks);

            tempNumOfAttacks--;

        }
    }

    public void WaveAttack(int attackIndex)
    {
       GameObject wave = Instantiate(bossWaveAttacks[attackIndex], waveAttackSpots[attackIndex].position, Quaternion.identity);
       wave.GetComponent<Rigidbody>().AddForce(waveAttackSpots[attackIndex].forward * attackWaveForce, ForceMode.Impulse);
    }
}
