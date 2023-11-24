using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
public class XMoveComponent : XComponent 
{
    
    public override void Init()
    {
        
    }
    public override void RegisterEvents()
    {
        base.RegisterEvents();
        Register<CastSkillArgs>( OnCastSkill);
        Register<MoveArgs>(OnMove);
    }

    private void OnCastSkill(EventArgs e)
    {
        
    }
    private void OnMove(EventArgs e)
    {
        _entity.ApplyMove((e as MoveArgs).dir);
    }
}