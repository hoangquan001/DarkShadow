using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
public class XMoveComponent : XComponent, IState
{
    public StateType stateId => StateType.Move;

    public override void Init()
    {

    }
    public override void RegisterEvents()
    {
        base.RegisterEvents();
        // Register<CastSkillArgs>( OnCastSkill);
        Register<MoveEventArgs>(OnMove);
    }

    private void OnCastSkill(EventArgs e)
    {

    }
    private void OnMove(EventArgs e)
    {
        MoveEventArgs args = e as MoveEventArgs;
        
        _entity.stateMachine.TransitionTo(this);
    }

    public void OnEnter() { }
    public void UpdateAction()
    {
    }
    public void FixedUpdateAction()
    {
        _entity.animator.SetFloat("SpeedRun", Mathf.Abs((float)_entity.rb2d.velocity.x));
        _entity.Move(new Vector2((int)_entity.movement.x, 1), 10);
    }
    public void OnExit()
    {

    }

}