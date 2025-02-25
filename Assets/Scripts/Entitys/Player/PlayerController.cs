
using System.Collections;
using UnityEngine;
[RequireComponent(typeof(EntityAttribute))]
public class PlayerController : EntityController
{
    public float DashSpeed = 50;
    public GameObject DustEffect;
    public GameObject ExplosionEffect;
    public GameObject TrailEffect;
    XSkillMgr skillMgr;


    Coroutine coroutine;


    public override void Awake()
    {
        base.Awake();
        skillMgr = new XSkillMgr(this);

        DustEffect.SetActive(false);
        // ExplosionEffect.SetActive(false);
        TrailEffect.SetActive(false);
    }
    // Start is called before the first frame update

    public override void Start()
    {
        base.Start();
        XSkillCore xSkillCore = new XSkillCoreBuilder(this)
        .SetID(6)
        .SetAnimationClip(Resources.Load<AnimationClip>("Animation/Player/Dash"))
        .SetAction(null)
        .SetActionTime(0f)
        .SetCountDown(1f)
        .SetSkillType(XSkillType.Dash)
        .Build();
        skillMgr.AddSkill(xSkillCore);
        xSkillCore = new XSkillCoreBuilder(this)
        .SetID(7)
        .SetAnimationClip(Resources.Load<AnimationClip>("Animation/Player/Attack"))
        .SetActionTime(0f)
        .SetAction(null)
        .SetCountDown(0.5f)
        .LockMovement()
        .SetSkillType(XSkillType.NormalAttack)
        .Build();

        skillMgr.AddSkill(xSkillCore);
        var animationClip = Resources.Load<AnimationClip>("Animation/Player/MagicFire");
        xSkillCore = new XSkillCoreBuilder(this)
        .SetID(8)
        .SetAnimationClip(animationClip)
        .SetActionTime(0.5f * animationClip.length)
        .SetAction(null)
        .SetCountDown(1f)
        .LockMovement()
        .SetSkillType(XSkillType.FireMagic)
        .Build();
        xSkillCore.onDoSkill += OnSkillTrigger;

        skillMgr.AddSkill(xSkillCore);
    }
    bool CheckSkill()
    {

        if (Input.GetKeyDown(KeyCode.V))
        {
            return skillMgr.CastSkill(6);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            return skillMgr.CastSkill(7);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            return skillMgr.CastSkill(8);
        }
        return false;
    }
    bool CheckJump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            CurState = EntityState.Jump;
            return true;
        }
        return false;
    }

    public bool IsGrounded()
    {
        Vector2 center = (Vector2)_collider.bounds.center;
        Vector2 boundExtends = _collider.bounds.size;
        boundExtends.y /= 2;
        boundExtends.x -= 0.1f;
        RaycastHit2D hit = Physics2D.BoxCast(center + boundExtends * Vector2.down / 2, boundExtends, 0f, Vector2.down, 0.2f, GroundLayer);
        return (hit.collider != null);
    }


    public void FireBullet(XProjectile xProjectile)
    {
        xProjectile.Fire(this, transform.Find("FirePoint").position, 50, Vector2.right * (int)Face, 10);
        xProjectile.SetLayer(gameObject.layer);
    }


    public override void Update()
    {

        base.Update();
        Velocity.x = Input.GetAxisRaw("Horizontal");
        Velocity.y = Input.GetAxisRaw("Vertical");
        animator?.SetBool("Ground", IsGrounded());
        DustEffect.SetActive(IsGrounded() && CurState == EntityState.Run);
        CheckJump();
        CheckSkill();
        skillMgr.Update();
    }



    protected override void OnStateEnter(EntityState State)
    {
        base.OnStateEnter(State);

        switch (State)
        {
            case EntityState.Run:

                break;
            case EntityState.Jump:
                rb2d.gravityScale = 4;
                rb2d.velocity = Vector2.zero;
                rb2d.AddForce(force: new Vector2(0, 1000));
                animator.SetBool("Jump", true);
                animator.SetBool("Fall", false);
                break;
            case EntityState.Falling:
                animator.SetBool("Jump", false);
                animator.SetBool("Fall", true);
                break;
            case EntityState.Death:
                // StartCoroutine(Death());
                break;
            case EntityState.Idle:
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
                // animator.SetFloat("SpeedRun", value: 0);
                break;
            case EntityState.Attack:
                XSkillCore core = skillMgr.skillCasting;
                switch (core.SkillType)
                {
                    case XSkillType.Dash:
                        coroutine = StartCoroutine(DashHandler(5));
                        TrailEffect.SetActive(true);
                        break;
                    case XSkillType.NormalAttack:
                        break;
                    case XSkillType.FireMagic:
                        break;
                }
                break;
        }
    }

    IEnumerator DashHandler(float range)
    {
        Vector3 startPos = transform.position;
        float maxMove = startPos.x + range * (int)Face;
        while ((transform.position - startPos).magnitude < range)
        {
            rb2d.velocity = Vector3.zero;
            float x = (int)Face * DashSpeed * Time.fixedDeltaTime;
            // x = Mathf.Clamp(x, maxMove + transform.position.x, maxMove + transform.position.x);
            rb2d.position += new Vector2(x, 0);
            yield return new WaitForFixedUpdate();
        }
        ;
    }
    protected override void OnStateExit(EntityState state)
    {
        base.OnStateExit(state);
        switch (state)
        {
            case EntityState.Attack:
                XSkillCore core = skillMgr.skillCasting;
                switch (core.SkillType)
                {
                    case XSkillType.Dash:
                        TrailEffect.SetActive(false);
                        StopCoroutine(coroutine);
                        coroutine = null;
                        break;
                    case XSkillType.NormalAttack:
                        break;
                    case XSkillType.FireMagic:
                        break;
                }
                break;
            case EntityState.Jump:
            case EntityState.Falling:
                animator.SetBool("Jump", false);
                animator.SetBool("Fall", false);
                break;
        }
    }

    protected override void OnStateUpdate(EntityState state)
    {

                // Debug.LogError(message: state);
        base.OnStateUpdate(state);
        switch (state)
        {
            case EntityState.Jump:

                Move(Velocity * Speed);
                if (rb2d.velocity.y < 0)
                {
                    CurState = EntityState.Falling;
                }
                break;

            case EntityState.Falling:
                Move(Velocity * Speed);
                if (IsGrounded())
                {

                    CurState = EntityState.Idle;
                }
                break;
            case EntityState.Run:
                animator.SetFloat("SpeedRun", Mathf.Abs(Velocity.x * Speed));
                Move(Velocity * Speed);
                if (Velocity.x == 0.0f)
                {

                    CurState = EntityState.Idle;
                }
                if (rb2d.velocity.y < -0.01)
                {
                    CurState = EntityState.Falling;
                }
                break;
            case EntityState.Idle:
                animator.SetFloat("SpeedRun", 0);
                if (Velocity.x != 0)
                {
                    CurState = EntityState.Run;
                }
                if (rb2d.velocity.y < -0.01)
                {
                    CurState = EntityState.Falling;
                }
                break;
            case EntityState.Attack:
                XSkillCore core = skillMgr.skillCasting;
                switch (core.SkillType)
                {
                    case XSkillType.Dash:
                        break;
                    case XSkillType.NormalAttack:
                        break;
                    case XSkillType.FireMagic:
                        break;
                }
                if(core.LockMovement ) Velocity.x = 0;
                animator.SetFloat("SpeedRun", Mathf.Abs(Velocity.x * Speed));
                Move(Velocity * Speed);
                break;
        }
    }

    private bool OnSkillTrigger(XSkillCore xSkillCore)
    {
        GameObject go = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Object/MagicFire"));
        FireBullet(go.GetComponent<XProjectile>());
        return true;
    }

    public void HandleSkill(XSkillCore core)
    {

    }
    public override void Move(Vector3 motion)
    {
        float xVelocity = rb2d.velocity.x;
        xVelocity = motion.x;
        rb2d.velocity = new Vector2(xVelocity, rb2d.velocity.y);
        if (motion.x != 0)
        {
            Rotation(direct: motion.x > 0 ? 1 : -1);
        }

    }


    private void FixedUpdate()
    {

    }


    public override void Die()
    {
        base.Die();
        CurState = EntityState.Death;
    }

}