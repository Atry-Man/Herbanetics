using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class UIManager : MonoBehaviour
{
    [Header("Score Elements")]
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text endGameScore;
    private const string ScoreVar = "Score";
    public int ScoreCount { get; set; }

    private void OnEnable()
    {
        ScorePickup.AddScore += AddScore;
        PlayerDamage.ScoreUpdate += UpdateScore;
    }

    private void OnDisable()
    {
        ScorePickup.AddScore -= AddScore;
        PlayerDamage.ScoreUpdate -= UpdateScore;
    }

    private void Awake()
    {
        if (!PlayerPrefs.HasKey(ScoreVar))
        {
            ScoreCount = 0;
        }
        else
        {
            LoadScore();

        }
    }



    void AddScore(int score)
    {   
        ScoreCount+=score;
        scoreText.text = ScoreCount.ToString();
    }

    void UpdateScore()
    {
        endGameScore.text = ScoreCount.ToString();
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt(ScoreVar, ScoreCount);
        PlayerPrefs.Save();
    }

    public void LoadScore()
    {
        int currentScore = PlayerPrefs.GetInt(ScoreVar, 0);
        scoreText.text = currentScore.ToString();
        ScoreCount = currentScore;
    }

    public void RestScore()
    {
        PlayerPrefs.SetInt (ScoreVar, 0);
    }

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
