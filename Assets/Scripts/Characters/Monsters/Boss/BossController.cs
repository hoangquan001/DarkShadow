using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BossState
{
    talk1, idle, hidden, appear, jumpAttack, spinAttack, landing, talk2, dead
}
public class BossController : MonoBehaviour, IMonsters
{
    // Start is called before the first frame update
    public Vector2 LeftPoint;
    public Vector2 RightPoint;
    public Vector2 UpPoint;
    public float SpinSkillCD = 2f;
    public int SpinSkillRepetion = 2;
    public int JumpSkillRepetion = 4;
    public float landingTime = 3;
    public float HP;

    public float invisibleTime = 0.4f;

    public float spinForce = 2000;
    public float spinDamage = 20;
    public float speedJump = 50;

    public Transform spinPoint;

    public GameObject player;
    public GameObject SpinPrefab;
    public GameObject appearanceVFX;
    public GameObject landdingVFX;
    public AudioClip invisibleClip;
    public AudioClip quakeClip;




    float SpinSkillTimer = 0;
    float landingTimer = 0;

    bool isFinish = false;
    bool DelayOnAir = false;

    int SpinSkillRepetionCouter = 0;
    int JumpSkillRepetionCouter = 0;
    bool isDamagePlayer = false;


    int state = 0;

    BossState bossState;


    float invisibleTimer = 0;
    // Rigidbody2D rb2d;
    Animator anim;
    [SerializeField] BoxCollider2D box2d;
    SpriteRenderer spriteR;
    AudioSource _audioSource;
    BossTalkingController bossTalking;
    void Awake()
    {
        transform.position = LeftPoint;
        // rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteR = GetComponent<SpriteRenderer>();
        box2d = GetComponent<BoxCollider2D>();
        _audioSource = GetComponent<AudioSource>();
        //delayTimer = 0;
        bossState = BossState.talk1;
        SpinSkillTimer = 2;

        bossTalking = GetComponent<BossTalkingController>();
        bossTalking.talk1();
    }


    int direct = -1;
    public void takeDamage(float damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            bossState = BossState.talk2;
            bossTalking.talk2();
        }
    }
    public void setDirect(int direct)
    {
        //this.direct = direct;
    }
    void next()
    {
        state = state + 1 < 3 ? state + 1 : 0;
        SpinSkillRepetionCouter = JumpSkillRepetionCouter = 0;
        hiddenState();
    }

    float getPlayePosisionX()
    {
        return player.transform.position.x;
    }
    bool isTouchGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, box2d.bounds.size.y / 2 + 0.1f, LayerMask.GetMask("Ground"));
        return (hit.collider != null);
    }
    // Update is called once per frame
    void showBoss(bool value)
    {
        spriteR.enabled = value;
        box2d.enabled = value;

    }


    void SpinState()
    {

        Vector2 pos = state == 0 ? LeftPoint : RightPoint;
        direct = state == 0 ? -1 : 1;
        transform.position = pos;
        {

            bossState = BossState.spinAttack;
        }


    }


    void JumpState()
    {

        if (JumpSkillRepetionCouter >= JumpSkillRepetion)
        {
            bossState = BossState.landing;
        }
        else
        {
            Vector2 pos = UpPoint;
            pos.x = getPlayePosisionX();

            DelayOnAir = true;
            Instantiate(appearanceVFX, pos, Quaternion.identity);
            bossState = BossState.jumpAttack;
            JumpSkillRepetionCouter += 1;
            transform.position = pos;

        }




    }
    void hiddenState()
    {
        showBoss(false);
        bossState = BossState.hidden;
        _audioSource.PlayOneShot(invisibleClip);
        Instantiate(appearanceVFX, transform.position, Quaternion.identity);

    }

    public void setBossState(BossState bossState)
    {
        this.bossState = bossState;
    }



    void Update()
    {

        if (bossState == BossState.talk1)
        {
            bool isDone = bossTalking.isDone;
            if (isDone) bossState = BossState.appear;
        }
        if (bossState == BossState.talk2)
        {

            bool isDone = bossTalking.isDone;
            if (isDone) bossState = BossState.dead;
        }

        if (bossState == BossState.appear)
        {
            //Control attack State
            if ((state == 0 || state == 1))
            {
                SpinState();
            }

            else if (state == 2)
            {

                JumpState();
            }
        }



        if (bossState == BossState.spinAttack)
        {
            SpinSkillTimer -= Time.deltaTime;
            if (SpinSkillTimer < 0)
            {
                SpinSkillTimer = SpinSkillCD;
                anim.SetTrigger("spinAttack");
                SpinSkillRepetionCouter += 1;
                isFinish = false;
            }

            if (SpinSkillRepetionCouter >= SpinSkillRepetion && isFinish)
            {
                next();
            }
        }


        if (bossState == BossState.jumpAttack)
        {
            anim.SetBool("jumpAttack", true);

            if (isDamagePlayer == false)
            {
                if (isTouchPlayer())
                {
                    damagePlayer();
                    isDamagePlayer = true;
                }
            }

            if (isTouchGround())
            {
                CameraShake.Instance.OnStartShake();
                Instantiate(landdingVFX, transform.position - new Vector3(0, -3), Quaternion.identity);
                _audioSource.PlayOneShot(quakeClip, 0.25f);
                isDamagePlayer = false;
                anim.SetBool("jumpAttack", false);
                hiddenState();

            }
            else
            if (DelayOnAir == false)
            {
                transform.position -= new Vector3(0, Time.deltaTime * speedJump);
            }

        }
        if (bossState == BossState.landing)
        {
            if (landingTimer > landingTime)
            {
                next();
                landingTimer = 0;
            }
            else
            {
                landingTimer += Time.deltaTime;
            }

        }


        if (bossState == BossState.hidden)
        {


            if (invisibleTimer < invisibleTime)
                invisibleTimer += Time.deltaTime;
            else
            {
                invisibleTimer = 0;

                bossState = BossState.appear;
                showBoss(true);
            }
        }

        if (bossState == BossState.dead)
        {
            SceneController.Instance.backToStartMenu();
        }



        updateFlip();

    }

    void updateFlip()
    {
        transform.localScale = new Vector2(direct, 1);
    }
    bool isTouchPlayer()
    {
        RaycastHit2D hit = Physics2D.BoxCast((Vector2)transform.position + box2d.offset, box2d.bounds.size, 0, Vector2.down, 0, LayerMask.GetMask("Player"));
        return (hit.collider != null);
    }
    //private void OnDrawGizmos()
    //{
    //    RaycastHit2D hit = Physics2D.BoxCast(transform.position, box2d.bounds.size, 0, Vector2.down, 0, LayerMask.GetMask("Player"));
    //    print(hit.collider);
    //}
    // AnimationEvent
    void unDelayOnAir()
    {
        DelayOnAir = false;
    }

    void damagePlayer()
    {
        player.GetComponent<PlayerCharacter>().takeDamage(20);

    }
    void finishAttack()
    {
        isFinish = true;

    }
    void SpinAttack()
    {
        bossState = BossState.spinAttack;

    }
    void createSpin()
    {
        GameObject spinObject = Instantiate(SpinPrefab, spinPoint.position, Quaternion.identity);
        SpinController projectile = spinObject.GetComponent<SpinController>();
        projectile.setDirect(-(int)transform.localScale.x);
        projectile.setDamage(spinDamage);
        projectile.addForce(spinForce);
    }


}
