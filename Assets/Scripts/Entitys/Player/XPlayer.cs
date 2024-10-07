using System.Collections;
using System.Collections.Generic;
using DarkShadow;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Scripting.APIUpdating;


// public enum PlayerSkill { None, Dash, Attack, FireMagic };
// public enum PlayerMovement { Idle, Run, Jump, Fall };

public class XPlayer : XEntity
{
    // public GameObject DustVFX;
    // public GameObject ExplosionVFX;
    // public GameObject TrailVFX;

    public override void OnCreate()
    {
        entityType = EntityType.Player;
        // moveArgs  = GetEventArgs<MoveArgs>();

    }
    public override void InitComponent()
    {
        // AddXComponent(new PlayerControlComponent());
        XComponent xSkillComponent = AddXComponent(XComponent.CreateComponent<XSkillComponent>());
        var xMoveComponent = AddXComponent(XComponent.CreateComponent<XMoveComponent>());
        var xIdleComponent = AddXComponent(XComponent.CreateComponent<XIdleComponent>());
        var xDashComponent = AddXComponent(XComponent.CreateComponent<XDashComponent>());
        var xJumpComponent = AddXComponent(XComponent.CreateComponent<XJumpComponent>());
        stateMachine.addState(StateType.Idle, xIdleComponent as IState);
        stateMachine.addState(StateType.Move, xMoveComponent as IState);
        stateMachine.addState(StateType.Dash, xDashComponent as IState);
        stateMachine.addState(StateType.Jump, xJumpComponent as IState);
        stateMachine.setCurrentState(StateType.Idle);
        // stateMachine.addState(StateType.Jump, xSkillComponent as IState);
    }

    void CheckDash()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {

            DashEventArgs @event = XEventArgsMgr.GetEventArgs<DashEventArgs>();
            @event.DashRange = 10;
            @event.DashSpeed = 50;
            @FireEvent(@event);
        }
    }
    void checkJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            JumpArgs jumpArgs = XEventArgsMgr.GetEventArgs<JumpArgs>();
            @FireEvent(jumpArgs);
        }

    }

    public override void Update()
    {
        base.Update();
        Vector2 dir = Vector2.zero;
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");

        switch (stateMachine.CurrentState)
        {
            case StateType.Idle:
                if (dir.x != 0)
                {
                    MoveArgs moveArgs = XEventArgsMgr.GetEventArgs<MoveArgs>();
                    moveArgs.direction = dir;
                    @FireEvent(moveArgs);
                }
                CheckDash();
                checkJump();
                break;
            case StateType.Move:
                if (dir.x == 0)
                {
                    IdleEventArgs IdleArgs = XEventArgsMgr.GetEventArgs<IdleEventArgs>();
                    IdleArgs.dir = dir;
                    @FireEvent(IdleArgs);
                }
                else if (dir.x != (int)face)
                {
                    MoveArgs moveArgs = XEventArgsMgr.GetEventArgs<MoveArgs>();
                    moveArgs.direction = dir;
                    @FireEvent(moveArgs);
                }
                CheckDash();
                checkJump();

                break;
            case StateType.Dash:

                break;
            case StateType.Jump:
                CheckDash();
                // if (IsGrounded() && rb2d.velocity.y == 0)
                // {
                //     IdleEventArgs IdleArgs = XEventArgsMgr.GetEventArgs<IdleEventArgs>();
                //     IdleArgs.dir = dir;
                //     @FireEvent(IdleArgs);
                // }

                break;
            default:
                break;
        }
    }



    // public void Dash(Vector2 dir, float DashRange, float DashSpeed)
    // {

    //     rb2d.velocity = Vector3.zero;
    //     rb2d.MovePosition(transform.position + new Vector3( DashSpeed*dir.x, 0)* Time.fixedDeltaTime);

    // }

}