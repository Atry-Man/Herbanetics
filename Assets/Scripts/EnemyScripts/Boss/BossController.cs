using System.Collections;
using UnityEngine;
using System;

public class BossController : MonoBehaviour
{
    [SerializeField] GameObject bossObject;
    [SerializeField] float bossSpawnDelay = 5f;
    public event Action StartFight;
    private void Start()
    {
        StartCoroutine(BossSpawnDisappearRoutine());
    }
    public IEnumerator BossSpawnDisappearRoutine()
    {
       
        // Spawn the boss
        bossObject.SetActive(true);
        yield return new WaitForSeconds(bossSpawnDelay);
        // Disappear the boss
        bossObject.SetActive(false);
        StartFight?.Invoke();     
    }
}
