using System;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
public class XJumpComponent : XComponent, IState
{
    public StateType stateId => StateType.Jump;
    private float timer = 0.0f;
    private float preGravity = 0 ;
    public override void Init()
    {

    }
    public override void RegisterEvents()
    {
        base.RegisterEvents();
        // Register<CastSkillArgs>( OnCastSkill);
        Register<JumpArgs>(OnJump);
    }

    private void OnCastSkill(EventArgs e)
    {

    }
    private void OnJump(EventArgs e)
    {
        _entity.stateMachine.TransitionTo(StateType.Jump);
    }

    public void OnEnter()
    {
        preGravity = _entity.rb2d.gravityScale;
        _entity.rb2d.gravityScale = 4;
        _entity.AddForce(force: new Vector2(0, 1000));
        _entity.animator.SetBool("Jump", true);

        timer = 0.0f;
    }
    public void UpdateAction()
    {
        timer += Time.deltaTime;
        _entity.Move(new Vector2((int)_entity.movement.x, 1), 10);
    }
    public void OnExit()
    {
        _entity.rb2d.gravityScale = preGravity;
        _entity.animator.SetBool("Jump", false);
    }
    public void FixedUpdateAction()
    {
    }
}