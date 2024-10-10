using System;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
public class XSkillComponent : XComponent, IState
{
    XSkillCore SkillCore = null;
    public bool IsCastingSkill => SkillCore != null && SkillCore.IsCastingSkill;
    public StateType stateId => StateType.Skill;
    public override void Init()
    {

        XSkillCore xSkillCore = new XSkillCoreBuilder()
        .SetAnimationClip(Resources.Load<AnimationClip>("Animation/Player/Dash"))
        .SetActionTime(0f)
        .SetCountDown(2f)
        .SetID(6)
        .SetCaster(_entity)
        .SetSkillType(XSkillType.Dash)
        .Build();
        _entity.SkillMgr.AddSkill(xSkillCore);
        xSkillCore = new XSkillCoreBuilder()
        .SetAnimationClip(Resources.Load<AnimationClip>("Animation/Player/Attack"))
        .SetActionTime(0f)
        .SetCountDown(2f)
        .SetID(7)
        .SetCaster(_entity)
        .SetSkillType(XSkillType.NormalAttack)
        .Build();
        _entity.SkillMgr.AddSkill(xSkillCore);
        var animationClip = Resources.Load<AnimationClip>("Animation/Player/MagicFire");
        xSkillCore = new XSkillCoreBuilder()
        .SetAnimationClip(animationClip)
        .SetActionTime(0.5f* animationClip.length)
        .SetCountDown(2f)
        .SetID(8)
        .LockMovement()
        .SetCaster(_entity)
        .SetSkillType(XSkillType.FireMagic)
        .Build();
        _entity.SkillMgr.AddSkill(xSkillCore);
    }


    public override void RegisterEvents()
    {
        base.RegisterEvents();
        Register<SkillEventArgs>(OnCastSkill);
    }

    private void OnCastSkill(EventArgs e)
    {
        if(IsCastingSkill) return;
        SkillEventArgs args = e as SkillEventArgs;
        SkillCore = _entity.SkillMgr.GetSkill((uint)args.SkillId);
        SkillCore.Fire();
        OnEnter();


    }


    public void OnEnter()
    {
        _entity.Stop();
        _entity.OverrideAnimationClip("Skill", SkillCore.SkillClip);
        _entity.animator.SetBool("Attack", true);
        _entity.LockMovement = SkillCore.NeedLockMovement;
        
    }

    public void UpdateAction()
    {
       
    }
    public override void Update()
    {
        if (SkillCore != null && !SkillCore.IsCastingSkill)
        {
            OnExit();
        }
    }

    public void FixedUpdateAction()
    {
    }

    public void OnExit()
    {

        _entity.animator.SetBool("Attack", false);
        _entity.LockMovement = false;

        if (SkillCore != null)
        {
            SkillCore.Finish();
            SkillCore = null;
        }
    }
}