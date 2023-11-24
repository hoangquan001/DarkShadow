using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
public class XSkillComponent : XComponent 
{
    XSkillCore Skill = null;

    public override void RegisterEvents()
    {
        base.RegisterEvents();
        Register<CastSkillArgs>( OnCastSkill);
    }

    private void OnCastSkill(EventArgs e)
    {
        CastSkillArgs args = e as CastSkillArgs;
        XSkillCore xSkillCore =  _entity.SkillMgr.GetSkill((uint)args.skillId);
        if(_entity.SkillMgr.CanCastSkill((uint)args.skillId))
        {
            Skill = xSkillCore;
            Skill.Fire();
        }
    }

    public override void Update()
    {
        
    }
}