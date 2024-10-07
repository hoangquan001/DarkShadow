using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
public class XJumpComponent : XComponent, IState
{
    public StateType stateId => StateType.Jump;

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

        JumpArgs args = e as JumpArgs;
        // _entity.Rotation(args.direction.x > 0 ? 1 : -1);
        // _entity.Move(args.direction, 10);
    }

    public void OnEnter()
    {
        // _entity.animator.SetBool("Jump", true);
        _entity.AddForce(new Vector2(0, 500));
    }
    public void UpdateAction()
    {
        _entity.animator.SetBool("Jump", _entity.rb2d.velocity.y > 0);
        _entity.animator.SetBool("Fall", _entity.rb2d.velocity.y <= 0);
        if (_entity.rb2d.velocity.y <= 0 && _entity.IsGrounded())
        {
            _entity.Idle();
        }

    }
    public void OnExit()
    {
        _entity.animator.SetBool("Jump", false);
        _entity.animator.SetBool("Fall", false);

    }

}