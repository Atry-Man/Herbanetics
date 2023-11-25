using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class StompFront : MonoBehaviour
{
    [Header("Stomp Variables")]
    [SerializeField] Transform stompPos;
    [SerializeField] Transform stompPos2;
    [SerializeField] Transform stompPos3;
    [SerializeField] float maxDistance;
    private bool canStomp;

    [Header("External Variables")]
    [SerializeField] PlayerController playerController;
    private const string StompTrigger = "Stomp";
    [SerializeField] StompFrontSO stompFrontSO;
    [SerializeField] Animator playerAnim;
    [SerializeField] GameObject reticle;
    InputDection inputDection;

    private void Awake()
    {
        inputDection = GameObject.FindObjectOfType<InputDection>();
    }

    public void StartStomp(InputAction.CallbackContext callbackContext)
    {   
        if(inputDection.CanUseControls)
        {
            if (callbackContext.action.triggered && !canStomp)
            {
                canStomp = true;
                playerAnim.SetTrigger(StompTrigger);

            }
        }
       
    }

    public void StompAttack()
    {
        int numStomps = SkillManager.instance.waveSkillLevel + 1;

        if (SkillManager.instance.waveSkillLevel >= 2)
            numStomps = 3;

        for (int i = 0; i < numStomps; i++)
        {
            switch (i)
            {
                case 0:
                    SpawnStomp(stompPos, stompFrontSO.abilityPrefab);
                    break;
                case 1:
                    SpawnStomp(stompPos2, stompFrontSO.abilityPrefab2);
                    break;
                case 2:
                    SpawnStomp(stompPos3, stompFrontSO.abilityPrefab3);
                    break;

            }
        }
    }

    private IEnumerator StompAttack(GameObject stompObject, Transform targetEnemy)
    {
        float elapsedTime = 0f;

        while (elapsedTime < stompFrontSO.abilityDuration)
        {
            float t = elapsedTime / stompFrontSO.abilityDuration;
            float currentWaveSize = Mathf.Lerp(0f, stompFrontSO.stompDesiredSize, t);

            if (targetEnemy != null)
            {
                Vector3 directionToTarget = (targetEnemy.position - stompObject.transform.position).normalized;
                reticle.transform.position = targetEnemy.transform.position + directionToTarget;
                stompObject.transform.position += directionToTarget * stompFrontSO.stompSpeed * Time.deltaTime;

            }
            else
            {
                stompObject.transform.position += stompFrontSO.stompSpeed * Time.deltaTime * stompObject.transform.forward;
            }

            stompObject.transform.localScale = Vector3.one * currentWaveSize;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(stompObject);

        yield return new WaitForSeconds(stompFrontSO.abilityCooldown);
        canStomp = false;
    }

    private bool FindClosestEnemy(Vector3 position, out Transform target)
    {
        target = null;
        float closestDistance = maxDistance;

       
        EnemySetup[] enemies = FindObjectsOfType<EnemySetup>();
        BossTag[] bossTags = FindObjectsOfType<BossTag>();


        foreach (EnemySetup enemy in enemies)
        {
            float distance = Vector3.Distance(position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                target = enemy.transform;
            }
        }

        foreach (BossTag boss in bossTags)
        {

            float distance = Vector3.Distance(position, boss.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                target = boss.transform;
            }
        }

        return target != null;
    }

    private void SpawnStomp(Transform stompPosition, GameObject stomp)
    {
        stomp = Instantiate(stomp, stompPosition.position, stompPosition.rotation);
        stomp.transform.localScale = Vector3.zero;

        if(stompFrontSO.canAutoAim)
        {
            if (FindClosestEnemy(stompPosition.position, out Transform targetEnemy))
            {
                reticle.SetActive(true);
                StartCoroutine(StompAttack(stomp, targetEnemy));
                Invoke(nameof(TurnOffReticle), 0.5f);
            }
        }
        else
        {
            StartCoroutine(StompAttack(stomp, null));
        }


    }

    void TurnOffReticle()
    {
        reticle.SetActive(false);
    }

}
