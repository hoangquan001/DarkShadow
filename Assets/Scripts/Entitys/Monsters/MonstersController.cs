// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;


// public class MonstersController : MonoBehaviour,IMonsters
// {
//     enum State { idle, walk, attack, dead };
//     // Start is called before the first frame update
//     [Header("Moving position")]
//     [SerializeField] private Vector2 attackRange;
//     [SerializeField] private Vector2 attackArea;

//     [SerializeField] private float speedMove =10f;
//     [SerializeField] private float RangeMoveX=10f;

//     public float damage = 5f;
//     public float health = 100f;
//     public float attackCD = 1f;
//     public float idleTime = 1f;
//     public float backwardDistance = 1f;

//     [SerializeField] GameObject ownedItem;

//     private Vector3 originPosition;
//     State monsterState = State.idle;
//     LayerMask playerMask;
//     int direct=1;

//     // Update is called once per frame

//     private Rigidbody2D rb2d;
//     private Animator ani;
//     private PlayerCharacter player=null;
//     private float RangeMoveXCounter = 0;
//     private bool isAtack=false;
//     private float idleTimer = 0;
//     private float attackCDtimer = 0;
//     private float backwardDistanceCounter;
//     void Awake()
//     {
//         playerMask = LayerMask.GetMask("Player");
//         rb2d = GetComponent<Rigidbody2D>();
//         ani = GetComponent<Animator>();
//         originPosition = transform.position;
//         direct = Random.Range(-1,1)<0?-1:1;
//         backwardDistanceCounter = backwardDistance;


//     }
//     //AnimationEvent
//     public void DestroyObject()
//     {
//         Destroy(this.gameObject);

//         Instantiate(ownedItem, transform.position,Quaternion.identity);
//     }

//     // Update is called once per frame
//     bool PlayerInArea()
//     {
//         RaycastHit2D hit1 = Physics2D.BoxCast(transform.position, attackArea, 0, Vector2.left, 0, playerMask);
//         bool hasPlayer = (hit1.collider != null);
//         if (hasPlayer)
//         {
//             player = hit1.transform.GetComponent<PlayerCharacter>();
//         }
//         return hasPlayer;
//     }
//     bool PlayeInSight()
//     {
//         RaycastHit2D hit2 = Physics2D.Raycast(transform.position, Vector2.right * direct, attackRange.x, playerMask);
//         return (hit2.collider != null);
//     }


//      public void setDirect(int direct)
//     {
//         this.direct = direct;
//     }

//      public void takeDamage(float damage)
//     {
//         health -= damage;
//         backwardDistanceCounter = 0f;
//         idleTimer = idleTime/2;
//     }

//     bool delayOutRange = false;

//      void Update()
//     {

//         ani.SetInteger("state", (int)monsterState);
//         if (health <= 0)
//         {
//             DestroyObject();
//         }

//         updateFlip();

//         monsterState = State.walk;
//         bool isPlayerInArea = PlayerInArea();

//         if (backwardDistanceCounter < backwardDistance)
//         {
//             float deltaD = Time.deltaTime * speedMove *4;
//             if (!checkOutOfRange())
//             {
//                 transform.position = (transform.position + new Vector3(deltaD * -direct, 0));
//             }
//             backwardDistanceCounter += deltaD;

//             return;
//         }

//         idleTimer -= Time.deltaTime;
//         if (idleTimer > 0)
//         {
//             monsterState = State.idle;
//             return;
//         }


//         if (isPlayerInArea)
//         {
//             isAtack = true;
//             if (PlayeInSight())
//             {
//                 attackCDtimer += Time.deltaTime;

//                 if (attackCDtimer > attackCD)
//                 {
//                     attackCDtimer = 0;
//                     monsterState = State.attack;
//                 }
//             }
//             else
//             {
//                 move(player.transform.position);
//             }
//             delayOutRange = true;
//         }
//         else
//         {
//             if (delayOutRange == true)
//             {
//                 idleTimer = idleTime;
//                 delayOutRange = false;
//                 return;
//             }

//             if (isAtack)
//             {
//                 move(originPosition);
//                 if (Vector2.Distance(transform.position, originPosition) < 0.1)
//                 {
//                     transform.position = originPosition;
//                     isAtack = false;
//                 }
//             }

//             if(isAtack==false)
//             {

//                 float detalDistanceX = Time.deltaTime * speedMove;
//                 RangeMoveXCounter += detalDistanceX;

//                 if (RangeMoveXCounter < RangeMoveX)
//                 {
//                     transform.position += new Vector3(detalDistanceX * direct, 0);
//                 }

//                 else
//                 {
//                     if ( Vector2.Distance(transform.position, originPosition) <0.5f)
//                     {
//                         transform.position = originPosition;
//                     }
//                     else
//                     {
//                         direct *= -1;
//                         idleTimer = idleTime / 2;
//                     }
//                     RangeMoveXCounter = 0;


//                 }

//             }
//         }



//     }

//     private void OnDrawGizmos()
//     {
//         Gizmos.color = Color.red;
//         //Gizmos.DrawWireCube(transform.position+ new Vector3(attackRange.x/2,0,0)*direct, attackRange);
//         Gizmos.DrawLine(transform.position, transform.position + new Vector3(attackRange.x, 0) * direct);
//         Gizmos.color = Color.black;
//         Gizmos.DrawLine(transform.position - new Vector3(RangeMoveX, 0.5f), transform.position + new Vector3(RangeMoveX,-0.5f) );
//         Gizmos.color = Color.blue;
//         Gizmos.DrawWireCube(transform.position, attackArea);

//     }
//     //Animation Event
//     public void damagePlayer()
//     {
//         if (PlayeInSight())
//         {
//             player.takeDamage(damage);
//         }
//     }
//     void updateFlip()
//     {
//         transform.localScale = new Vector2(direct, 1);
//     }
//     bool checkOutOfRange()
//     {
//         if (transform.position.x < originPosition.x - RangeMoveX || transform.position.x > originPosition.x + RangeMoveX)
//             return true;
//         return false;
//     }
//     void move(Vector3 nextPosMove)
//     {
//         transform.position =Vector2.MoveTowards(this.transform.position, nextPosMove, Time.deltaTime * speedMove);
//         direct = this.transform.position.x < nextPosMove.x ? 1 : -1;

//     }

// }
