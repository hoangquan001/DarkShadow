using System.Collections;
using System.Collections.Generic;
using DarkShadow;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Scripting.APIUpdating;


// public enum PlayerSkill { None, Dash, Attack, FireMagic };
// public enum PlayerMovement { Idle, Run, Jump, Fall };

public class XPlayer: XEntity 
{
    public override void OnCreate()
    {

        // moveArgs  = GetEventArgs<MoveArgs>();

    }
    public override void InitComponent()
    {
        // AddXComponent(new PlayerControlComponent());
        XComponent xSkillComponent = AddXComponent(XComponent.CreateComponent<XSkillComponent>());
        var xMoveComponent = AddXComponent(XComponent.CreateComponent<XMoveComponent>());
        var xIdleComponent = AddXComponent(XComponent.CreateComponent<XIdleComponent>());
        var xDashComponent = AddXComponent(XComponent.CreateComponent<XDashComponent>());
        stateMachine.addState(StateType.Idle, xIdleComponent as IState);
        stateMachine.addState(StateType.Move, xMoveComponent as IState);
        stateMachine.addState(StateType.Dash, xDashComponent as IState);
        // stateMachine.addState(StateType.Jump, xSkillComponent as IState);
    }
    public override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.V))
        {
        }
        Vector2 dir = Vector2.zero;
        dir.x =  Input.GetAxisRaw("Horizontal");
        dir.y =  Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(KeyCode.C))
        {
            DashEventArgs @event = XEventArgsMgr.GetEventArgs<DashEventArgs>();
            @event.DashRange = 10;
            @event.DashSpeed = 50;
            @FireEvent(@event);
        }   
        if(Input.GetKeyDown(KeyCode.V))
        {
            
        }   


        if(dir.x == 0)
        {
            IdleEventArgs IdleArgs = XEventArgsMgr.GetEventArgs<IdleEventArgs>();
            IdleArgs.dir = dir;
            @FireEvent(IdleArgs);
        }
        else
        {
            MoveArgs moveArgs = XEventArgsMgr.GetEventArgs<MoveArgs>();
            moveArgs.dir = dir;
            @FireEvent(moveArgs);
        }
    }



    // public void Dash(Vector2 dir, float DashRange, float DashSpeed)
    // {
        
    //     rb2d.velocity = Vector3.zero;
    //     rb2d.MovePosition(transform.position + new Vector3( DashSpeed*dir.x, 0)* Time.fixedDeltaTime);
        
    // }

}