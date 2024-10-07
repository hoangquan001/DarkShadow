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
        Register<MoveArgs>(OnMove);
    }

    private void OnCastSkill(EventArgs e)
    {

    }
    private void OnMove(EventArgs e)
    {
        _entity.stateMachine.TransitionTo(StateType.Move);

        MoveArgs args = e as MoveArgs;
        _entity.Rotation(args.direction.x > 0 ? 1 : -1);
        // _entity.ApplyMove(args.dir);
    }

    public void OnEnter() { }
    public void UpdateAction()
    {
        _entity.Move(new Vector2((int)_entity.face, 0), 10);
        _entity.animator.SetFloat("SpeedRun", Mathf.Abs((float)_entity.rb2d.velocity.x));
    }
    public void OnExit()
    {

    }

}