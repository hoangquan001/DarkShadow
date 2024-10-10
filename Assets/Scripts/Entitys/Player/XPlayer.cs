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

    public override void OnCreate()
    {
        entityType = EntityType.Player;
        // MoveEventArgs  = GetEventArgs<MoveEventArgs>();
        Attributes.SetAttributeValue(AttributeType.Damage, 10);
        Attributes.SetAttributeValue(AttributeType.Defense, 10);
        Attributes.SetAttributeValue(AttributeType.MaxHP, 100);
        Attributes.SetAttributeValue(AttributeType.MaxMP, 100);
        Attributes.SetAttributeValue(AttributeType.DashRange, 5);
        Attributes.SetAttributeValue(AttributeType.SpeedDash, 100);
        Attributes.SetAttributeValue(AttributeType.SpeedJump, 10);
        Attributes.SetAttributeValue(AttributeType.SpeedFall, 10);
        Attributes.SetAttributeValue(AttributeType.SpeedRun, 10);
    }
    public override void InitComponent()
    {
        // AddXComponent(new PlayerControlComponent());
        var xSkillComponent = AddXComponent(XComponent.CreateComponent<XSkillComponent>());
        var xMoveComponent = AddXComponent(XComponent.CreateComponent<XMoveComponent>());
        var xIdleComponent = AddXComponent(XComponent.CreateComponent<XIdleComponent>());
        var xDashComponent = AddXComponent(XComponent.CreateComponent<XDashComponent>());
        var xJumpComponent = AddXComponent(XComponent.CreateComponent<XJumpComponent>());
        var xFallComponent = AddXComponent(XComponent.CreateComponent<XFallComponent>());
        stateMachine.SetDefaultState(xIdleComponent as IState);
    }

    bool CheckSkill()
    {

        if (Input.GetKeyDown(KeyCode.V))
        {
            return SkillMgr.CastSkill(6);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            return SkillMgr.CastSkill(7);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            return SkillMgr.CastSkill(8);
        }
        return false;
    }
    bool CheckJump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded())
        {
            JumpEventArgs jumpArgs = XEventArgsMgr.GetEventArgs<JumpEventArgs>();
            jumpArgs.IsJumping = true;
            FireEvent(jumpArgs);
            return true;

        }
        return false;
    }
    bool CheckFall()
    {
        if (rb2d.velocity.y < 0 && !IsGrounded())
        {
            FallEventArgs fallArgs = XEventArgsMgr.GetEventArgs<FallEventArgs>();
            FireEvent(fallArgs);
            return true;

        }
        return false;

    }
    public override void FireBullet(XProjectile xProjectile)
    {
        xProjectile.Fire(this, transform.Find("FirePoint").position, 50, Vector2.right*(int)face, 10);
        xProjectile.SetLayer(gameObject.layer);
    }

    public override void Update()
    {
        base.Update();
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // if (!IsGrounded() && rb2d.velocity.y < 0)
        // {
        //     JumpArgs jumpArgs = XEventArgsMgr.GetEventArgs<JumpArgs>();
        //     jumpArgs.IsJumping = false;
        //     @FireEvent(jumpArgs);
        //     return;
        // }
        switch (stateMachine.CurStateType)
        {
            case StateType.Idle:
                if (movement.x != 0)
                {
                    MoveEventArgs MoveEventArgs = XEventArgsMgr.GetEventArgs<MoveEventArgs>();
                    MoveEventArgs.direction = movement;
                    FireEvent(MoveEventArgs);
                }
                CheckSkill();
                CheckJump();
                CheckFall();
                break;
            case StateType.Move:
                if (movement.x == 0)
                {
                    IdleEventArgs IdleArgs = XEventArgsMgr.GetEventArgs<IdleEventArgs>();
                    IdleArgs.dir = movement;
                    FireEvent(IdleArgs);
                }
                else if (movement.x != (int)face)
                {
                    MoveEventArgs MoveEventArgs = XEventArgsMgr.GetEventArgs<MoveEventArgs>();
                    MoveEventArgs.direction = movement;
                    FireEvent(MoveEventArgs);
                }
                CheckSkill();
                CheckJump();
                CheckFall();
                break;
            case StateType.Dash:
                // CheckFall();
                break;
            case StateType.Jump:
                CheckSkill();
                CheckFall();
                if (IsGrounded() && Time.time - stateMachine.startTime > 0.5f)
                {
                    IdleEventArgs IdleArgs = XEventArgsMgr.GetEventArgs<IdleEventArgs>();
                    FireEvent(IdleArgs);
                }
                break;
            case StateType.Fall:
                CheckSkill();
                if (IsGrounded())
                {
                    IdleEventArgs IdleArgs = XEventArgsMgr.GetEventArgs<IdleEventArgs>();
                    FireEvent(IdleArgs);
                }

                break;
            case StateType.Skill:
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