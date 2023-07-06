using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class StompFront : MonoBehaviour
{
    [Header("Stomp Variables")]
    [SerializeField] Transform stompPos;
    private bool canStomp;

    [Header("External Variables")]
    [SerializeField] PlayerController playerController;
    private const string StompTrigger = "Stomp";
    [SerializeField] StompFrontSO stompFrontSO;
    [SerializeField] Animator playerAnim;

    public void StartStomp(InputAction.CallbackContext callbackContext)
    {
        if(callbackContext.action.triggered && !canStomp)
        {
            canStomp = true;
            playerAnim.SetTrigger(StompTrigger);
            GameObject stomp = Instantiate(stompFrontSO.abilityPrefab, stompPos.position, stompPos.rotation);
            stomp.transform.localScale = Vector3.zero;
            StartCoroutine(StompAttack(stomp));
        }
    }

   private  IEnumerator StompAttack(GameObject stompObject)
    {
        float elapsedTime = 0f;
        

        while (elapsedTime < stompFrontSO.abilityDuration)
        {
            float t = elapsedTime / stompFrontSO.abilityDuration;

            float currentWaveSize = Mathf.Lerp(0f, stompFrontSO.stompDesiredSize, t);
           
           
            stompObject.transform.position += stompFrontSO.stompSpeed * Time.deltaTime * stompObject.transform.forward;

            stompObject.transform.localScale = Vector3.one * currentWaveSize;

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Destroy(stompObject);
       
        yield return new WaitForSeconds(stompFrontSO.abilityCooldown);
        canStomp = false;
    }


}
