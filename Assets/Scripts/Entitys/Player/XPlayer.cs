using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;


// public enum PlayerSkill { None, Dash, Attack, FireMagic };
// public enum PlayerMovement { Idle, Run, Jump, Fall };

public class XPlayer: XEntity 
{
    Rigidbody2D rb2d ;
    public override void Awake()
    {
        base.Awake();
        rb2d  = GetComponent<Rigidbody2D>();
    }
    public override void OnCreate()
    {

        // moveArgs  = GetEventArgs<MoveArgs>();

    }
    public override void InitComponent()
    {
        // AddXComponent(new PlayerControlComponent());
        AddXComponent(XComponent.CreateComponent<XSkillComponent>());
        AddXComponent(XComponent.CreateComponent<XMoveComponent>());
        AddXComponent(XComponent.CreateComponent<XIdleComponent>());
        AddXComponent(XComponent.CreateComponent<XDashComponent>());
    }
    public override void Update()
    {
        base.Update();
        if(Input.GetKeyDown(KeyCode.V))
        {
        }
        Vector2 dir = Vector2.zero;
        dir.x =  Input.GetAxisRaw("Horizontal");
        dir.y =  Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(KeyCode.C))
        {
            DashEventArgs @event = XSingleton<XEventArgsMgr>.Instance.GetEventArgs<DashEventArgs>();
            @event.DashRange = 10;
            @event.DashSpeed = 50;

        }   
        if(Input.GetKeyDown(KeyCode.V))
        {
            
        }   


        if(dir.x == 0)
        {
            IdleEventArgs IdleArgs = XSingleton<XEventArgsMgr>.Instance.GetEventArgs<IdleEventArgs>();
            IdleArgs.dir = dir;
            @FireEvent(IdleArgs);
        }
        else
        {
            MoveArgs moveArgs = XSingleton<XEventArgsMgr>.Instance.GetEventArgs<MoveArgs>();
            moveArgs.dir = dir;
            @FireEvent(moveArgs);
        }
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Move(MoveDir, 10);
    }
    public void Move(Vector2 dir, float speed)
    {
        float xVelocity = rb2d.velocity.x;
        xVelocity = dir.x * speed;
        rb2d.velocity = new Vector2(xVelocity, rb2d.velocity.y);
    }
    // public void Dash(Vector2 dir, float DashRange, float DashSpeed)
    // {
        
    //     rb2d.velocity = Vector3.zero;
    //     rb2d.MovePosition(transform.position + new Vector3( DashSpeed*dir.x, 0)* Time.fixedDeltaTime);
        
    // }

}