
using UnityEngine;

public delegate bool EndSkillCondition();
public class XSkillCore
{
    public bool IsCastingSkill
    {
        get
        {
            return SkillTimer > 0;
        }

    }

    public uint ID;
    public float CountDown = 5;
    float CDTimer = 0;
    float SkillTime 
    {
        get
        {
            return _skillClip.length;
        }
    }
    float SkillTimer = 0;

    public bool IsCD
    {
        get
        {
            return CDTimer > 0;
        }
    }
    XSkillType SkillType;
    private AnimationClip _skillClip;
    public AnimationClip SkillClip
    {
        get
        {
            return _skillClip;
        }
        set
        {
            _skillClip = value;
        }
    }
    public XSkillCore()
    {
        SkillClip = Resources.Load<AnimationClip>("Animation/Player/MagicFire");

    }

    public void Update()
    {
        if (CDTimer > 0)
        {
            CDTimer -= Time.deltaTime;
        }
        if (SkillTimer > 0)
        {
            SkillTimer -= Time.deltaTime;
        }
    }

    public void Begin()
    {
        CDTimer = CountDown;
        SkillTimer = SkillTime;
    }
    public void Cancel()
    {
        CDTimer = 0;
        SkillTimer = 0;
    }

    public void Finish()
    {
    }

    public void Fire()
    {
        Begin();
    }

    
    // public EndSkillCondition()
}

public class XSkillCoreBuilder
{
    XSkillCore xSkillCore = new XSkillCore();
    public XSkillCoreBuilder() { }
    public XSkillCoreBuilder SetAnimationClip(AnimationClip clip)
    {
        xSkillCore.SkillClip = clip;
        return this;
    }
    public XSkillCoreBuilder SetCountDown(float countDown)
    {
        xSkillCore.CountDown = countDown;
        return this;
    }

    public XSkillCoreBuilder SetID(uint id)
    {
        xSkillCore.ID = id;
        return this;
    }

    public XSkillCore Build()
    {
        return xSkillCore;
    }
}