using System;
using System.Collections.Generic;
using DarkShadow;
using UnityEngine;
public class XIdleComponent : XComponent, IState
{
    public StateType stateId => StateType.Idle;

    public override void RegisterEvents()
    {
        base.RegisterEvents();
        Register<IdleEventArgs>(OnEventAction);
    }

    private void OnEventAction(EventArgs e)
    {
        _entity.stateMachine.TransitionTo(this);

    }

    public void OnEnter()
    {
        _entity.Stop();
        _entity.animator.SetFloat("SpeedRun", 0);
    }

    public void OnExit()
    {
    }

    public void UpdateAction()
    {
        // _entity.ApplyMove(dir);
    }

    public void FixedUpdateAction()
    {
    }
}