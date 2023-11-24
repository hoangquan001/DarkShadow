using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NPCController : MonoBehaviour
{
    
    [SerializeField] GameObject dialog;
    [SerializeField] float TalkTime=0.1f;
    [SerializeField] GameObject icon;
    [TextArea][SerializeField] List<string> listTexts;
    bool isShow = false;
    bool playerInSight = false;
    int idx=0;
    Text textBox;
    // Use this for initialization

    int state = 0;
    bool keyNext = false;

    bool isTextRunning=false;


    BoxCollider2D box2d;
    void Awake()
    {
        box2d = GetComponent<BoxCollider2D>();
        dialog.SetActive(false);
    }    
    void Start()
    {
        textBox = dialog.GetComponentInChildren<Text>();
    }

    void checkPlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(box2d.bounds.center, box2d.bounds.size, 0, Vector2.down, 0, LayerMask.GetMask("Player"));
        if (hit.collider)
        {
            playerInSight = true;
            icon.SetActive(true);
        }
        else
        {
            playerInSight = false;
            icon.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        checkPlayerInSight();

        if (Input.GetKeyDown(KeyCode.F)&&playerInSight)
        {
            isShow = true;
            dialog.SetActive(true);
            keyNext = true;

            if (isTextRunning == false && idx >= listTexts.Count)
            {

                dialog.SetActive(false);
                isShow = false;
                state = 9;
            }
        }
       

        if (isShow)
        {
            if (state == 0)
            {
                if (isTextRunning == false && idx == 3)
                {
                    state = 1;
                }
                showTextHandle();

              
            }
            if (state == 1)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    state = 2;
                    keyNext = true;
                }

            }
            if(state == 2)
            {

                showTextHandle();
                if (isTextRunning == false)
                {
          
                    state = 3;
                }
            }
            if (state == 3)
            {
                if (PlayerInput.Instance.getAttackInput())
                {
                    state = 4;
                    keyNext = true;
                }
            }
            if (state == 4)
            {
                showTextHandle();
                if (isTextRunning == false)
                {
                    PlayerInput.Instance.setActive(true);
                    state = 5;
                }

            }

            if (state == 5)
            {
                if (PlayerInput.Instance.getDashInput())
                {
                    keyNext = true;
                    state = 6;

                }
            }

            if (state == 6)
            {
                showTextHandle();
                if (isTextRunning == false)
                {
                    PlayerInput.Instance.setActive(true);
                    state = 7;
                }

            }

            if (state == 7)
            {
                if (PlayerInput.Instance.getMagicFireInput())
                {
                    keyNext = true;
                    state = 8;
                }
            }

            if (state == 8)
            {
                showTextHandle();
            }



        }
    }


    void showTextHandle()
    {
        if (keyNext)
        {
            keyNext = false;
            if (isTextRunning == true)
            {
                isTextRunning = false;
                return;
            }
            StartCoroutine(printSequence(listTexts[idx++]));
        }
    }

    IEnumerator printSequence(string ss)
    {
        
        isTextRunning = true;
        for (int i = 1; i <= ss.Length; i++)
        {
            if (isTextRunning == false)
            {
                textBox.text = ss;
                yield break;

            }
            textBox.text = ss.Substring(0, i);
            yield return new WaitForSeconds(TalkTime);
        }
        isTextRunning = false;
    }
}
