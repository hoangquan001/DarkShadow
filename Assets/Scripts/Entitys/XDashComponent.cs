using System;
using System.Collections.Generic;
using DarkShadow;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
public class XDashComponent : XComponent, IState
{
    private float speed = 100f;
    private float range = 5f;

    private Vector3 startPos;
    private Vector3 curPos;

    private GameObject vfx;
    private float disableTimer;
    public StateType stateId => StateType.Dash;
    public override void Init()
    {
        vfx = _entity.transform.Find("VFX/Trail").gameObject;
    }

    public override void RegisterEvents()
    {
        base.RegisterEvents();
        Register<DashEventArgs>(OnEventAction);
    }

    private void OnEventAction(EventArgs e)
    {
        DashEventArgs args = e as DashEventArgs;
        range = args.DashRange;
        speed = args.DashSpeed;
        _entity.OverrideAnimationClip("Skill", args.AnimationClip);

        _entity.stateMachine.TransitionTo(this);
    }
    public void OnEnter()
    {

        vfx.SetActive(true);
        startPos = _entity.transform.position;
        curPos = startPos;
        disableTimer = 0.1f;
        _entity.animator.SetBool("Attack", true);
    }

    public void UpdateAction()
    {

    }

    public void OnExit()
    {
        vfx.SetActive(false);
        _entity.animator.SetBool("Attack", false);
    }

    public void FixedUpdateAction()
    {
        if ((curPos - startPos).magnitude >= range)
        {
            _entity.animator.SetInteger("SkillState", 0);
            _entity.Stop();
            if (disableTimer > 0f)
            {

                disableTimer -= Time.fixedDeltaTime;
            }
            else
            {
                _entity.Idle();
            }
        }
        else
        {
            _entity.rb2d.velocity = Vector3.zero;
            float x = (int)_entity.face * speed * Time.deltaTime;
            curPos += new Vector3(x, 0);
            _entity.rb2d.MovePosition(_entity.Possition + new Vector3(x, 0));
        }
    }
}