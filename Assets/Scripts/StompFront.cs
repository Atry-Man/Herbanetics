using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StompFront : MonoBehaviour
{
    [Header("Stomp Variables")]
    [SerializeField] GameObject stompEffectPrefab;
    [SerializeField] float stompDesiredSize;
    [SerializeField] float stompSpeed;
    [SerializeField] float stompDuration;
    [SerializeField] Transform stompPos;
    private bool isAttacking;


    public void StartStomp(InputAction.CallbackContext callbackContext)
    {
        if(callbackContext.action.triggered && !isAttacking)
        {
            isAttacking = true;
            GameObject stomp = Instantiate(stompEffectPrefab, stompPos.position, Quaternion.identity);
            StartCoroutine(StompAttack(stomp));
        }
    }

   private  IEnumerator StompAttack(GameObject stompObject)
    {
        float elapsedTime = 0f;
        Vector3 waveScale = Vector3.zero;

        while (elapsedTime < stompDuration)
        {
            float t = elapsedTime / stompDuration;

            
            waveScale = Vector3.Lerp(Vector3.zero, Vector3.one * stompDesiredSize, t);

            // Move the wave along its path
            stompObject.transform.position += stompSpeed * Time.deltaTime * transform.forward;

          
            stompObject.transform.localScale = waveScale;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

       
        waveScale = Vector3.one * stompDesiredSize;
        stompObject.transform.localScale = waveScale;

        isAttacking = false;
    }



}
