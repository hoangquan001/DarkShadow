using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MovingObject : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Vector2[] Nodes=new  Vector2[2];

    [SerializeField] private bool isLoop = false;

    [SerializeField] private float platformSpeed=10f;
    [SerializeField] private float waitTime=1f;
    public enum MovingPlatformType { CIRCLE,LINE }
    public MovingPlatformType platformType;
    private int nextIdx;

    private bool isActive = true;

    //Rigidbody2D _rigidbody;
    private float m_waitTime;
    private int direct =1;

    void Start()
    {
        //_rigidbody = GetComponent<Rigidbody2D>();
        transform.position = Nodes[0];
        nextIdx = 1;
        m_waitTime = 0f;
    }
    public bool IsActive()
    {
        return isActive;
    }
    void movePlatform()
    {
        if (m_waitTime > 0)
        {
            m_waitTime -= Time.fixedDeltaTime;
            return;
        }

        if (nextIdx == 0 && isLoop == false)
        {
            isActive = false;
            return;
        }



        Vector2 nextPos=Vector2.MoveTowards(this.transform.position, Nodes[nextIdx], platformSpeed * Time.fixedDeltaTime);
        transform.position = nextPos;

        if (nextPos== Nodes[nextIdx])
        {
            m_waitTime = waitTime;

            switch (platformType)
            {
                case MovingPlatformType.CIRCLE:
                    nextIdx = (nextIdx + 1) < Nodes.Length ? nextIdx + 1 : 0;
                    break;
                case MovingPlatformType.LINE:
                    if (nextIdx + direct < 0 || nextIdx + direct > Nodes.Length - 1)
                    {
                        direct = -1 * direct;
                    }
                    nextIdx = nextIdx + direct;
                    break;
            }

        }

        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (isActive)
        {
            movePlatform();
        }
        else if (isLoop) isActive = true;

    }


}
