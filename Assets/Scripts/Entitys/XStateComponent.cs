using System;
using System.Collections.Generic;
using DarkShadow;
using Unity.Mathematics;
using UnityEngine;
public class XStateComponent : XComponent, IState
{
    public virtual StateType stateId => StateType.Idle;

    public override void RegisterEvents()
    {
        base.RegisterEvents();
        // Register<IdleEventArgs>(OnEventAction);
    }

    private void OnEventAction(EventArgs e)
    {
        _entity.stateMachine.TransitionTo(this);

    }

    public void OnEnter()
    {
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