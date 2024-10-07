using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
public enum Face { Right = 1, Left = -1 }
public enum EntityType { None = 0, Player = 1, NPC = 2 };
public class XEntity : XObject
{
    public StateMachine stateMachine;

    public Rigidbody2D rb2d;
    public Collider2D _collider;
    public Animator animator;
    private XSkillMgr _skillMgr;
    public Face face = Face.Right;

    public Vector2 movement = Vector2.zero;
    public XSkillMgr SkillMgr
    {
        get
        {
            return _skillMgr;
        }
    }
    public EntityType entityType = EntityType.None;
    private XAttributes _attributes = new XAttributes();
    public XAttributes Attributes
    {
        get
        {
            return _attributes;
        }
    }
    public XEntity()
    {
        stateMachine = new StateMachine(this);

    }
    public override void Awake()
    {
        base.Awake();
        rb2d = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }
    public override void OnCreate()
    {
        _skillMgr = new XSkillMgr();
    }
    public float groundDistance = 0.1f;


    public override void Update()
    {
        base.Update();
        animator.SetBool("Ground", IsGrounded());
        stateMachine.Update();

        // _skillMgr.Update();
    }

    public void ApplyMove(Vector2 dir)
    {
        movement = dir;
    }

    public void Rotation(int direct)
    {
        transform.rotation = Quaternion.Euler(0, direct > 0 ? 0 : 180, 0);
        face = direct > 0 ? Face.Right : Face.Left;
    }
    public void Dash(Vector2 dir, float DashSpeed)
    {
        rb2d.velocity = Vector3.zero;
        rb2d.MovePosition(transform.position + new Vector3(DashSpeed * dir.x, 0) * Time.fixedDeltaTime);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    public void Move(Vector2 dir, float speed)
    {
        float xVelocity = rb2d.velocity.x;
        xVelocity = dir.x * speed;
        rb2d.velocity = new Vector2(xVelocity, rb2d.velocity.y * dir.y);
        if (dir.x != 0)
        {
            Rotation(direct: dir.x > 0 ? 1 : -1);
        }

    }
    public void Stop()
    {
        rb2d.velocity = new Vector2(0, rb2d.velocity.y);
    }
    public void AddForce(Vector2 force)
    {
        rb2d.AddForce(force);
    }
    public bool IsGrounded()
    {
        LayerMask groundLayer = LayerMask.GetMask("Ground");
        Vector2 center = (Vector2)_collider.bounds.center;
        Vector2 boundExtends = _collider.bounds.size;
        boundExtends.y /= 2;
        boundExtends.x -= 0.1f;
        RaycastHit2D hit = Physics2D.BoxCast(center + boundExtends * Vector2.down / 2, boundExtends, 0f, Vector2.down, 0.2f, groundLayer);
        groundDistance = hit.distance;
        return (hit.collider == true);
    }

    public void Idle()
    {
        IdleEventArgs IdleArgs = XEventArgsMgr.GetEventArgs<IdleEventArgs>();
        @FireEvent(IdleArgs);
    }

}