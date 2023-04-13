using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazeController : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField] private GameObject laze;
    [SerializeField] private float Distance;
    [SerializeField] private float speed;
    [SerializeField] private float startAtTime;
    [SerializeField] private float CountDown;
    private float CountDownTimer;
    private float DistanceCounter=0;
    private bool isShotting = false;

    private float delay =1;
    private float delayTimer = 0;
    private SpriteRenderer lazeRenderer;
    void Start()
    {
       
    }
    private void Awake()
    {
        lazeRenderer = laze.GetComponent<SpriteRenderer>();
       
    }
    // Update is called once per frame

    private void onCollisionPlayer()
    {
        float radius = transform.rotation.eulerAngles.z * Mathf.PI / 180;
        Vector2 direct = new Vector2(Mathf.Sin(radius), -Mathf.Cos(radius));
        RaycastHit2D hit = Physics2D.BoxCast((Vector2)laze.transform.position + direct*new Vector2(laze.GetComponent<SpriteRenderer>().bounds.size.x / 2, laze.GetComponent<SpriteRenderer>().bounds.size.y/2),
            laze.GetComponent<SpriteRenderer>().bounds.size, 0, Vector2.down, 0, LayerMask.GetMask("Player"));
        if (hit.collider != null)
        {
           hit.collider.GetComponent<PlayerCharacter>().takeDamage(0.3f);
        }

 
   
    }
    void Update()
    {
        if (startAtTime > 0)
        {
            startAtTime -= Time.deltaTime;
            return;
        }


        CountDownTimer += Time.deltaTime;
        if(CountDownTimer> CountDown)
        {

            isShotting = true;
        }


        if (isShotting)
        {
            DistanceCounter += Time.deltaTime * speed;
            if (DistanceCounter < Distance)
            {
                lazeRenderer.size = new Vector2(DistanceCounter, 0.75f);
            }
            else
            {
                if (delayTimer > delay)
                {
                    lazeRenderer.size = new Vector2(0, 0.75f);
                    CountDownTimer = 0;
                    isShotting = false;
                    DistanceCounter = 0;
                    delayTimer = 0;
                }
                else
                    delayTimer += Time.deltaTime;

            }
        }

        onCollisionPlayer();
    }
    private void OnDrawGizmos()
    {
        onCollisionPlayer();
    }
}
