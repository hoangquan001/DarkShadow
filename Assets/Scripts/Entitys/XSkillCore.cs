
using UnityEngine;

public delegate bool EndSkillCondition();
public class XSkillCore
{
    public uint ID; 
    bool isCastingSkill;
    float CountDown = 0;
    float CDTimer = 0;

    float SkillTime = 1;
    float SkillTimer = 0;

    public bool IsCD => CDTimer > 0;
    XSkillType SkillType;

    public void UpdateCD()
    {
        if(CDTimer > 0)
        {
            CDTimer -= Time.deltaTime;
        }
        if(SkillTimer > 0)
        {
            SkillTimer -= Time.deltaTime;
        }else
        {
            Finish();
        }
        
    }

    public void Begin()
    {
        isCastingSkill = true;
        CDTimer = CountDown;
        SkillTimer = SkillTime;
    }
    
    public void Finish()
    {
        isCastingSkill = false;
    }

    public void Fire()
    {
        Begin();
    }
    public bool IsCastingSkill()
    {
        return isCastingSkill;
        
    }

    // public EndSkillCondition()
}