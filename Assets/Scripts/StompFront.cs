using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class StompFront : MonoBehaviour
{
    [Header("Stomp Variables")]
    [SerializeField] GameObject stompEffectPrefab;
    [SerializeField] float stompDesiredSize;
    [SerializeField] float stompSpeed;
    [SerializeField] float stompDuration;
    [SerializeField] float stompCooldown;
    [SerializeField] Transform stompPos;
    [SerializeField] Animator playerAnim;
    private bool canStomp;

    [Header("External Variables")]
    [SerializeField] PlayerController playerController;
    private const string StompTrigger = "Stomp";

    public void StartStomp(InputAction.CallbackContext callbackContext)
    {
        if(callbackContext.action.triggered && !canStomp)
        {
            canStomp = true;
            playerController.CanMove = false;
            playerController.StopMovement();
            playerAnim.SetTrigger(StompTrigger);
            GameObject stomp = Instantiate(stompEffectPrefab, stompPos.position, stompPos.rotation);
            stomp.transform.localScale = Vector3.zero;
            StartCoroutine(StompAttack(stomp));
        }
    }

   private  IEnumerator StompAttack(GameObject stompObject)
    {
        float elapsedTime = 0f;
        

        while (elapsedTime < stompDuration)
        {
            float t = elapsedTime / stompDuration;

            float currentWaveSize = Mathf.Lerp(0f, stompDesiredSize, t);
           
           
            stompObject.transform.position += stompSpeed * Time.deltaTime * stompObject.transform.forward;

            stompObject.transform.localScale = Vector3.one * currentWaveSize;

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Destroy(stompObject);
        playerController.CanMove= true;
        yield return new WaitForSeconds(stompCooldown);

        canStomp = false;
    }



}
