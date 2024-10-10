
using System;
using UnityEngine;

public delegate bool EndSkillCondition();
public class XSkillCore
{
    public uint ID;
    public Action action;
    public XEntity Caster { get; set; }
    public bool NeedLockMovement { get; set; } = false;
    public float CountDown = 5;
    float CDTimer = 0;
    float actionTimer = 0;
    public float actionTime = 0;
    public XSkillType SkillType;
    private AnimationClip _skillClip;
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

    public void Begin()
    {
        CDTimer = CountDown;
        SkillTimer = SkillTime;
        actionTimer = actionTime;
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

    private void DoSkillAction()
    {
        switch(SkillType)
        {
            case XSkillType.Dash:
                DashEventArgs args = XEventArgsMgr.GetEventArgs<DashEventArgs>();
                args.DashRange = 5;
                args.DashSpeed = 50;
                args.AnimationClip = SkillClip;
                Caster.FireEvent(args);
                break;
            case XSkillType.FireMagic:
                GameObject go = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Object/MagicFire"));
                Caster.FireBullet(go.GetComponent<XProjectile>());
                break;
        }
    }
    // public EndSkillCondition()
}

public class XSkillCoreBuilder
{
    XSkillCore xSkillCore ;
    public XSkillCoreBuilder() {
        xSkillCore = new XSkillCore();
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
        xSkillCore.NeedLockMovement = true;
        return this;
    }
    public XSkillCoreBuilder SetCaster(XEntity caster)
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
}