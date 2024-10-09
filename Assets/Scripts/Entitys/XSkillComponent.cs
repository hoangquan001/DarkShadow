using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
public class XSkillComponent : XComponent, IState
{
    XSkillCore SkillCore = null;

    public StateType stateId => StateType.Skill;

    public override void RegisterEvents()
    {
        base.RegisterEvents();
        Register<SkillEventArgs>( OnCastSkill);
    }

    private void OnCastSkill(EventArgs e)
    {
        SkillEventArgs args = e as SkillEventArgs;
        SkillCore =  _entity.SkillMgr.GetSkill((uint)args.SkillType);
        // if(_entity.SkillMgr.CanCastSkill((uint)args.SkillType))
        // {
         
        // }
        // _entity.OverrideAnimationClip(SkillCore.stateName, SkillCore.clip);
        _entity.stateMachine.TransitionTo(StateType.Skill);
    }


    public void OnEnter()
    {
        _entity.Stop();
        _entity.OverrideAnimationClip("Skill", SkillCore.SkillClip);
        _entity.animator.SetBool("Attack", true);
        SkillCore.Fire();
    }

    public void UpdateAction()
    {
        if(!SkillCore .IsCastingSkill)
        {
            _entity.Idle();
        }
    }

    public void FixedUpdateAction()
    {
    }

    public void OnExit()
    {
        _entity.animator.SetBool("Attack", false);

        if (SkillCore != null)
        {
            SkillCore.Finish();
            SkillCore = null;
        }
    }
}