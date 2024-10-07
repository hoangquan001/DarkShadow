using System;
using System.Collections.Generic;
using DarkShadow;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
public class XDashComponent : XComponent, IState
{

    private float startDash;
    private float speed = 100f;
    private float range;

    private Vector2 startPos;
    private Vector2 curPos;


    public StateType stateId => StateType.Dash;

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
    public void OnEnter()
    {
        startPos = _entity.transform.position;
        curPos = startPos;
        _entity.animator.SetInteger("SkillState", 1);

    }

    public void UpdateAction()
    {
        if ((curPos - startPos).magnitude > 5)
        {
            _entity.Idle();
        }
        else
        {
            curPos += new Vector2((int)_entity.face, 0) * speed * Time.deltaTime;
            _entity.Move(new Vector2((int)_entity.face, 0), speed);
        }
    }

    public void OnExit()
    {
        _entity.animator.SetInteger("SkillState", 0);

    }
}