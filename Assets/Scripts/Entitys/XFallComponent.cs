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
    private float preGravity = 0;

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
            _entity.stateMachine.TransitionTo(this);

    }

    public void OnEnter()
    {
        preGravity = _entity.rb2d.gravityScale;
        _entity.rb2d.gravityScale = 8;
        _entity.animator.SetBool("Fall", true);

    }
    public void UpdateAction()
    {


    }
    public void OnExit()
    {
        _entity.rb2d.gravityScale = preGravity;
        _entity.animator.SetBool("Fall", false);

    }

    public void FixedUpdateAction()
    {
        _entity.Move(new Vector2((int)_entity.movement.x, 1), 10);
    }
}