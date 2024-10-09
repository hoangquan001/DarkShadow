using System;
using System.Collections.Generic;
using DarkShadow;
using Unity.Mathematics;
using UnityEngine;
public class XStateComponent : XComponent, IState
{
    public StateType stateId => StateType.Idle;

    public override void RegisterEvents()
    {
        base.RegisterEvents();
        // Register<IdleEventArgs>(OnEventAction);
    }

    private void OnEventAction(EventArgs e)
    {
        // _entity.stateMachine.TransitionTo(StateType.Idle);
        // _entity.ApplyMove(Vector3.zero);

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