using System;
using System.Collections.Generic;
using DarkShadow;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
public class XDashComponent : XComponent,IState
{

    private float startDash;
    private float speed = 50f;
    private float range;

    public override void RegisterEvents()
    {
        base.RegisterEvents();
        Register<DashEventArgs>(OnEventAction);
    }

    private void OnEventAction(EventArgs e)
    {
        DashEventArgs args = e as DashEventArgs;
        _entity.stateMachine.TransitionTo(StateType.Dash);
    }
    private void OnEnter()
    {
        startDash = Time.time;
        
    }

    private void End()
    {
    }

    public void ActionUpdate()
    {

    }
    public override void FixedUpdate()
    {

        base.FixedUpdate();

        if (Time.time - startDash < 0.1f)
        {
            _entity.Dash(_entity.face == Face.Right ? Vector2.right : Vector2.left, speed);
        }
        
    }


}