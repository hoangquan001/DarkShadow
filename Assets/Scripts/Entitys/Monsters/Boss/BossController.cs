using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DarkShadow
{
    public enum BossFSM
    {
        talk1, idle, hidden, jumpAttack, spinAttack, stun, talk2, dead
    }

    public enum BossAttackFSM
    {

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
        public float stunTime = 3;
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
        float stunTimer = 0;

        bool isFinish = false;
        bool DelayOnAir = false;

        int SpinSkillRepetionCouter = 0;
        int JumpSkillRepetionCouter = 0;
        bool isDamagePlayer = false;


        int state = 1;

        public BossFSM bossState;


        // public StateMachine stateMachine;


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
            SpinSkillTimer = 2;
            // bossState = BossFSM.talk1;
            state = 1;
            if (bossState == BossFSM.talk1)
            {
                bossTalking = GetComponent<BossTalkingController>();
                bossTalking.talk1();
            }



            invisibleTimer = 0;
        }
        void intFSM()
        {
            // stateMachine = new StateMachine();

            // stateMachine.addState((int)BossFSM.idle, new BossIdleState(this));
            // stateMachine.addState((int)BossFSM.hidden, new BossIdleState(this));
            // stateMachine.addState((int)BossFSM.jumpAttack, new BossIdleState(this));
            // stateMachine.addState((int)BossFSM.spinAttack, new BossIdleState(this));
            // stateMachine.addState((int)BossFSM.talk2, new BossIdleState(this));
            // stateMachine.addState((int)BossFSM.dead, new BossIdleState(this));
            // stateMachine.addState((int)BossFSM.talk1, new BossIdleState(this));
            // bossState = BossFSM.talk1;
            // stateMachine.setCurrentState((int)bossState);


        }

        int direct = -1;
        public void takeDamage(float damage)
        {
            HP -= damage;

        }
        public void setDirect(int direct)
        {
            //this.direct = direct;
        }
        void nextSkill()
        {
            OnBossHidden();
            bossState = BossFSM.hidden;
            TimeManager.Instance.setTimeOut(invisibleTime, delegate
            {
                showBoss(true);
                chosseAttackState();
            });

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
            bossState = BossFSM.spinAttack;
        }
        void OnBossHidden()
        {
            showBoss(false);
            _audioSource.PlayOneShot(invisibleClip);
            Instantiate(appearanceVFX, transform.position, Quaternion.identity);

        }

        public void setBossState(BossFSM bossState)
        {
            this.bossState = bossState;
        }

        void chosseAttackState()
        {
            Vector2 pos = Vector2.zero;
            if (state == 0 || state == 1)
            {
                bossState = BossFSM.spinAttack;
                pos = state == 0 ? LeftPoint : RightPoint;
                direct = state == 0 ? -1 : 1;
                SpinSkillRepetionCouter = 0;
                state = state + 1 < 3 ? state + 1 : 0;
            }
            else
            {

                bossState = BossFSM.jumpAttack;
                pos = UpPoint;
                pos.x = player.transform.position.x;
                DelayOnAir = true;
                Instantiate(appearanceVFX, pos, Quaternion.identity);
            }


            // stateMachine.TransitionTo((int)bossState);
            transform.position = pos;

        }


        void ControlFMS()
        {
            if (HP <= 0)
            {
                bossState = BossFSM.talk2;
                bossTalking.talk2();
                return;
            }
            switch (bossState)
            {
                case BossFSM.talk1:
                    if (bossTalking.isDone)
                        nextSkill();

                    break;
                case BossFSM.talk2:
                    if (bossTalking.isDone) bossState = BossFSM.dead;
                    break;
                case BossFSM.idle:

                    break;
                case BossFSM.stun:
                    stunTimer += Time.deltaTime;
                    if (stunTimer >= stunTime)
                    {
                        nextSkill();
                        stunTimer = 0;
                    }
                    break;

                case BossFSM.spinAttack:
                    if (SpinSkillRepetionCouter >= SpinSkillRepetion && isFinish)
                    {
                        nextSkill();
                    }
                    break;
                case BossFSM.hidden:


                    break;
                case BossFSM.jumpAttack:
                    if (JumpSkillRepetionCouter >= JumpSkillRepetion)
                    {
                        JumpSkillRepetionCouter = 0;
                        bossState = BossFSM.stun;
                        state = 0;
                    }
                    break;
                case BossFSM.dead:
                    SceneController.Instance.backToStartMenu();
                    break;
            }
        }

        public void OnSpinAtack()
        {
            SpinSkillTimer -= Time.deltaTime;
            if (SpinSkillTimer < 0)
            {
                SpinSkillTimer = SpinSkillCD;
                anim.SetTrigger("spinAttack");
                SpinSkillRepetionCouter += 1;
                isFinish = false;
            }
        }
        void DoAction()
        {
            if (bossState == BossFSM.spinAttack)
            {
                OnSpinAtack();
            }


            if (bossState == BossFSM.jumpAttack)
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
                    JumpSkillRepetionCouter += 1;
                    if (JumpSkillRepetionCouter < JumpSkillRepetion)
                    {
                        nextSkill();
                    }
                }
                else
                if (DelayOnAir == false)
                {
                    transform.position -= new Vector3(0, Time.deltaTime * speedJump);
                }

            }
        }


        void Update()
        {

            ControlFMS();
            DoAction();
            updateFlip();

            // stateMachine.Update();

        }
        void FixedUpdate()
        {
            // stateMachine.FixedUpdate();
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
            bossState = BossFSM.spinAttack;

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
}