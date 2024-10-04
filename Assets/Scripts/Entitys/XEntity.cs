using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
public enum Face { Right, Left }
public class XEntity : XObject
{
    public StateMachine stateMachine = new StateMachine();

    protected Rigidbody2D rb2d;
    private XSkillMgr _skillMgr;
    public Face face = Face.Right;
    public XSkillMgr SkillMgr
    {
        get
        {
            return _skillMgr;
        }
    }
    [SerializeField] private XAttributes Attributes;
    public Vector2 MoveDir = Vector2.zero;
    public override void Awake()
    {
        base.Awake();
        rb2d = GetComponent<Rigidbody2D>();
    }
    public override void OnCreate()
    {
        _skillMgr = new XSkillMgr();
    }
    public XAttributes getAttributes()
    {
        return Attributes;
    }

    public override void Update()
    {
        base.Update();
        stateMachine.Update();

        // _skillMgr.Update();
    }

    public void ApplyMove(Vector2 dir)
    {
        MoveDir = dir;
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
        Move(MoveDir, 10);
    }
    public void Move(Vector2 dir, float speed)
    {
        float xVelocity = rb2d.velocity.x;
        xVelocity = dir.x * speed;
        rb2d.velocity = new Vector2(xVelocity, rb2d.velocity.y);
    }

    public void AddForce(Vector2 force)
    {
        rb2d.AddForce(force);
    }

}