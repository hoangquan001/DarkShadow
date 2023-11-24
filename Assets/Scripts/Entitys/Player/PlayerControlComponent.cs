using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerSkill { None, Dash, Attack, FireMagic };
public enum PlayerMovement { Idle, Run, Jump, Fall };

public class PlayerControlComponent : XComponent
{
    // Start is called before the first frame update
    [Header("Physical Value")]
    [SerializeField] private float speedJump = 5f;
    [SerializeField] private float speedRun = 10f;
    [SerializeField] private float jumpForce = 15;

    [SerializeField] private float GravityJump = 4;
    [SerializeField] private float GravityFall = 8;
    //[SerializeField] private float GravityBlink = 0;
    PlayerAudio playerAudio;


    [Header("Component")]

    //[SerializeField] private LayerMask groundLayer;
    private Rigidbody2D rb2d;
    [SerializeField] private BoxCollider2D PlayerCollider;
    public bool isGround = true;
    public bool isFalling = false;
    public bool isJumping = false;
    [HideInInspector]
    public PlayerMovement movementState;
    [HideInInspector]
    public PlayerSkill curSkill;

    bool isFacingRight = true;



    bool DashCD = false;
    bool AttackCD = false;
    bool MagicCD = false;

    float DashDistanceCounter = 0;

    bool JumpBtn = false;
    float numJump = 2;
    Vector2 moveInput;

    PlayerCharacter player = null;

    void Awake()
    {
        rb2d = host.GetComponent<Rigidbody2D>();
        PlayerCollider = host.GetComponent<BoxCollider2D>();
        player = host.GetComponent<PlayerCharacter>();
        curSkill = PlayerSkill.None;
        playerAudio = host.GetComponent<PlayerAudio>();
    }


    public override void Update()
    {
        updateStates();
        moveInput = Vector2.zero;
        //BlockControlPlayer

        ControlInput();


        if (moveInput.x != 0 && rb2d.velocity.x != 0)

            if (moveInput.x >= 0 != isFacingRight)
                Flip();

    }


    public override void FixedUpdate()
    {

        updateGravity();
        updateVelocity();
        Jump();
        Dash();

    }
    void FinishSkillEvent()
    {
        curSkill = PlayerSkill.None;
    }
    void ControlInput()
    {
        moveInput.x = PlayerInput.Instance.moveHorizontal();
        moveInput.y = PlayerInput.Instance.moveVertical();


        if (PlayerInput.Instance.getJumpDownInput())
        {
            JumpBtn = true;
        }

        // skill check ()
        if (curSkill == PlayerSkill.None)
        {


            //Dash skill
            if (PlayerInput.Instance.getDashInput() && !DashCD)
            {
                playerAudio.DashAudio();
                curSkill = PlayerSkill.Dash;
                DashCD = true;
                TimeManager.Instance.setTimeOut(player.DashCD, delegate { DashCD = false; });
                player.setActiveTrail(true);
            }

            //FireMagic skill

            if (PlayerInput.Instance.getMagicFireInput() && isGround && !MagicCD)
            {
                if (player.hasMana())
                {
                    playerAudio.MagicFireAudio();
                    player.playMagicSkill();
                    curSkill = PlayerSkill.FireMagic;
                    MagicCD = true;
                    TimeManager.Instance.setTimeOut(player.MagicCD, delegate { MagicCD = false; });
                }

            }

            //Attack skill

            if (PlayerInput.Instance.getAttackInput() && !AttackCD)
            {
                playerAudio.AttackAudio();
                curSkill = PlayerSkill.Attack;
                player.playAttackSkill();
                AttackCD = true;
                TimeManager.Instance.setTimeOut(player.attackCD, delegate { AttackCD = false; });
            }
        }
    }

    void Jump()
    {
        if (curSkill == PlayerSkill.FireMagic || curSkill == PlayerSkill.Dash) return;
        if (numJump > 0 && JumpBtn)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            numJump--;
            JumpBtn = false;
        }
    }

    void Dash()
    {
        if (curSkill != PlayerSkill.Dash) return;
        if (DashDistanceCounter <= player.dashRange)
        {
            int direct = isFacingRight ? 1 : -1;
            rb2d.velocity = Vector3.zero;
            rb2d.MovePosition(host.transform.position + new Vector3(direct * player.dashSpeed * Time.fixedDeltaTime, 0));
            DashDistanceCounter += (player.dashSpeed * Time.fixedDeltaTime);
        }
        else
        {
            player.setActiveTrail(false);
            DashDistanceCounter = 0;
            curSkill = PlayerSkill.None;
        }
    }
    void updateVelocity()
    {

        float xVelocity = rb2d.velocity.x;
        float speed = isGround == true ? speedRun : speedJump;
        //curAccelRun = Mathf.Clamp(curAccelRun, 0.5f, 1);
        xVelocity = moveInput.x * speed;

        if (moveInput.x == 0 || curSkill == PlayerSkill.FireMagic)
        {
            xVelocity = 0;
        }

        rb2d.velocity = new Vector2(xVelocity, rb2d.velocity.y);
    }
    void updateGravity()
    {
        float gravity = 1;
        if (isGround == false)
        {
            gravity = rb2d.velocity.y > 0 ? GravityJump : GravityFall;
        }
        if (curSkill == PlayerSkill.Dash)
        {
            gravity = 0;
        }
        rb2d.gravityScale = gravity;
    }

    bool checkGround()
    {
        LayerMask groundLayer = LayerMask.GetMask("Ground");
        Vector2 center = (Vector2)PlayerCollider.bounds.center;
        Vector2 boundExtends = PlayerCollider.bounds.size;
        boundExtends.y /= 2;
        boundExtends.x -= 0.1f;
        RaycastHit2D hit = Physics2D.BoxCast(center + boundExtends * Vector2.down / 2, boundExtends, 0f, Vector2.down, 0.2f, groundLayer);
        return (hit.collider == true);
    }
    void updateStates()
    {
        isFalling = false;
        isJumping = false;
        isGround = checkGround();
        if (isGround) numJump = 2;
        movementState = PlayerMovement.Idle;
        if (isGround == true && Mathf.Abs(rb2d.velocity.x) > 0.05)
        {
            movementState = PlayerMovement.Run;
        }
        if (isGround == false && rb2d.velocity.y < 0)
        {
            isFalling = true;
        }
        if (isGround == false && rb2d.velocity.y > 0)
        {

            isJumping = true;
        }
    }

    void Flip()
    {
        Vector3 loclaScale = host.transform.localScale;
        loclaScale.x *= -1;
        host.transform.localScale = loclaScale;
        isFacingRight = !isFacingRight;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.blue;

    //    Gizmos.DrawWireCube(PlayerCollider.bounds.center, PlayerCollider.bounds.size);
    //    Gizmos.color = Color.red;
    //    Vector2 center = (Vector2)PlayerCollider.bounds.center;
    //    Vector2 boundExtends = PlayerCollider.bounds.size;
    //    boundExtends.y /= 2;
    //    Gizmos.DrawWireCube(center + boundExtends * Vector2.down / 2, boundExtends);


    //}

    // Update is called once per frame
}

// void Jump()
// {

//     if (curSkill == PlayerSkill.FireMagic || curSkill == PlayerSkill.Dash)
//         return;
//     if (numJump > 0 && JumpBtn)
//     {
//         rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
//         rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
//         numJump--;
//         JumpBtn = false;
//     }
// }
// void Dash()
// {

//     if (curSkill == PlayerSkill.Dash)
//     {
//         if (DashDistanceCounter <= player.dashRange)
//         {
//             int direct = isFacingRight ? 1 : -1;
//             rb2d.velocity = Vector3.zero;
//             rb2d.MovePosition(transform.position + new Vector3(direct * player.dashSpeed * Time.fixedDeltaTime, 0));
//             DashDistanceCounter += (player.dashSpeed * Time.fixedDeltaTime);
//         }
//         else
//         {
//             player.setActiveTrail(false);
//             DashDistanceCounter = 0;
//             curSkill = PlayerSkill.None;
//         }
//     }
// }