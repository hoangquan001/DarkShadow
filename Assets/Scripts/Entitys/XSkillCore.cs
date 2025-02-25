
using System;
using UnityEngine;

public delegate bool EndSkillCondition();
public delegate bool OnSkillEnterState(XSkillCore xSkillCore);
public class XSkillCore
{
    public uint ID;
    public EntityController Caster { get; set; }
    public bool LockMovement { get; set; } = false;
    public float CountDown = 5;
    public float actionTime = 0;
    public XSkillType SkillType;
    public event OnSkillEnterState onDoSkill;
    public event OnSkillEnterState onBeginSkill;
    public event OnSkillEnterState onEndSkill;
    private float CDTimer = 0;
    private AnimationClip _skillClip;
    private float actionTimer = 0;
    float SkillTime
    {
        get
        {
            return _skillClip.length;
        }
    }
    float SkillTimer = 0;
    public bool IsCastingSkill
    {
        get
        {
            return SkillTimer > 0;
        }

    }
    public bool IsCD
    {
        get
        {
            return CDTimer > 0;
        }
    }

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
        actionTimer = float.MaxValue;
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
        if (actionTimer > 0)
        {
            actionTimer -= Time.deltaTime;
        }
        else 
        {
            DoSkillAction();
            actionTimer = float.MaxValue;
        }
    }

    private void Begin()
    {
        CDTimer = CountDown;
        SkillTimer = SkillTime;
        actionTimer = actionTime;
        onBeginSkill?.Invoke(this);
        Caster.OverrideAnimationClip("Skill", SkillClip);
        Caster.animator.SetBool("Attack", true);
    }

    public void Finish()
    {
        SkillTimer = 0;
        onEndSkill?.Invoke(this);
        Caster.animator.SetBool("Attack", false);

    }

    public void Fire()
    {
        Begin();
    }

    private void DoSkillAction()
    {
        onDoSkill?.Invoke(this);
    }
    // public EndSkillCondition()
}

public class XSkillCoreBuilder
{
    XSkillCore xSkillCore ;
    public XSkillCoreBuilder(EntityController entity) {
        xSkillCore = new XSkillCore();
        xSkillCore.Caster = entity;
    }
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
    public XSkillCoreBuilder SetActionTime(float time)
    {
        xSkillCore.actionTime = time;
        return this;
    }

    public XSkillCoreBuilder SetID(uint id)
    {
        xSkillCore.ID = id;
        return this;
    }
    public XSkillCoreBuilder LockMovement()
    {
        xSkillCore.LockMovement = true;
        return this;
    }
    public XSkillCoreBuilder SetCaster(EntityController caster)
    {
        xSkillCore.Caster = caster;
        return this;
    }
    public XSkillCoreBuilder SetSkillType(XSkillType  xSkillType)
    {
        xSkillCore.SkillType = xSkillType;
        return this;
    }

    public XSkillCore Build()
    {
        return xSkillCore;
    }
    public XSkillCoreBuilder SetAction(OnSkillEnterState action)
    {
        xSkillCore.onDoSkill += action;
        return this;
    }
}