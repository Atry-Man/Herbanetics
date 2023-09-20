using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    [Header("Projectile Skill")]
    public int projectileSkillLevel;

    [Header("Punch Skill")]
    public int punchSkillLevel;

    [Header("Wave Skill")]
    public int waveSkillLevel;

    [Header("Dash Skill")]
    public int dashLevel;
   
   

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    public void LevelUpProjectile()
    {
        projectileSkillLevel++;
    }

    public  void LevelUpWave()
    {
        waveSkillLevel++;
    }

    public void  LevelUpPunch()
    {
        punchSkillLevel++;
    }


}
