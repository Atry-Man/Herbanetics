using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    /*[Header("Ability UI")]
    [SerializeField] Image stompFillImage;
    [SerializeField] StompFrontSO stompFrontSO;
    private IEnumerator StartAbilityActiveCountdown(float abilityDuration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < abilityDuration)
        {
            float progress = elapsedTime / abilityDuration;
            stompFillImage.fillAmount = Mathf.Lerp(1f, 0f, progress);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        stompFillImage.fillAmount = 0f;
    }

    private IEnumerator StartAbilityCooldownCountdown(float abilityCoolDown)
    {
        float elapsedTime = 0f;

        while (elapsedTime < abilityCoolDown)
        {
            float progress = elapsedTime / abilityCoolDown;
            stompFillImage.fillAmount = Mathf.Lerp(0f, 1f, progress);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        stompFillImage.fillAmount = 1f;
    }

    public void AbilityActiveCountdownEventCaller()
    {

        StartCoroutine(StartAbilityActiveCountdown(stompFrontSO.abilityDuration));

    }

    public void AbilityModCooldownEventCaller()
    {
        StartCoroutine(StartAbilityCooldownCountdown(stompFrontSO.abilityCooldown));
    }*/

}
