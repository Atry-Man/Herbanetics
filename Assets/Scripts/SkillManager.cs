using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    [Header("Projectile Skill")]
    public int projectileSkillLevel = 0;

    [Header("Punch Skill")]
    public int punchSkillLevel = 0;

    [Header("Wave Skill")]
    public int waveSkillLevel = 0;

    [Header("Dash Skill")]
    public int dashLevel = 0;
   
   

    private void Awake()
    {

        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;

       
    }

    private void Start()
    {
        int projectileLvl = PlayerPrefs.GetInt("ProjectileLvl");
        projectileSkillLevel = Mathf.RoundToInt(projectileLvl);

        int punchSkill = PlayerPrefs.GetInt("PunchSkill");
        punchSkillLevel = Mathf.RoundToInt(punchSkill);

        int waveSkill = PlayerPrefs.GetInt("WaveSkill");
        waveSkillLevel = Mathf.RoundToInt(waveSkill);

        int dashSkill = PlayerPrefs.GetInt("DashSkill");
        dashLevel = Mathf.RoundToInt(dashSkill);
    }

    public void LevelUpProjectile()
    {
        PlayerPrefs.SetInt("StompSkill", projectileSkillLevel + 1); 

    }

    public  void LevelUpWave()
    {
        PlayerPrefs.SetInt("WaveSkill", waveSkillLevel + 1);
    }

    public void  LevelUpPunch()
    {
        PlayerPrefs.SetInt("PunchSkill", punchSkillLevel + 1);
    }

    public void LevelUpDash()
    {
        PlayerPrefs.SetInt("DashSkill", dashLevel + 1);
    }
}
