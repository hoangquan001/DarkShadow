using System;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
public class XFallComponent : XComponent, IState
{
    public StateType stateId => StateType.Fall;
    private float timer = 0.0f;
    public override void Init()
    {

    }
    public override void RegisterEvents()
    {
        base.RegisterEvents();
        Register<FallEventArgs>(OnFall);
    }

    private void OnFall(EventArgs e)
    {
        _entity.stateMachine.TransitionTo(StateType.Fall);

    }

    public void OnEnter()
    {
        _entity.animator.SetBool("Fall", true);

    }
    public void UpdateAction()
    {
        timer += Time.deltaTime;
        _entity.Move(new Vector2((int)_entity.movement.x, 1), 10);
        if (_entity.rb2d.velocity.y <= 0 && _entity.IsGrounded() && timer > 0.5f)
        {
            _entity.Idle();
        }

    }
    public void OnExit()
    {
        _entity.animator.SetBool("Fall", false);

    }

}