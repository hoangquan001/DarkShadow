using System;
using System.Collections.Generic;
using DarkShadow;
using Unity.Mathematics;
using UnityEngine;
public class XIdleComponent : XComponent, IState
{
    Vector3 dir = Vector3.zero;

    public override void RegisterEvents()
    {
        base.RegisterEvents();
        Register<IdleEventArgs>(OnEventAction);
    }

    private void OnEventAction(EventArgs e)
    {
        _entity.stateMachine.TransitionTo(StateType.Idle);
        dir = (e as IdleEventArgs).dir;

    }

    public void OnEnter()
    {
    }

    public void OnExit()
    {
    }

    public void UpdateAction()
    {
        _entity.ApplyMove(dir);
    }
}