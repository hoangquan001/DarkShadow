using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BossTalkingController : MonoBehaviour
{
    [TextArea][SerializeField] List<string> TextOnStart;
    [TextArea][SerializeField] List<string> TextOnEnd;
    [SerializeField] GameObject BoxText;
    Text textBox;
    // Use this for initialization
    int idx = 0;
    int state = 0;
    float delaytimer = 1;
    void Start()
    {
        textBox = BoxText.GetComponentInChildren<Text>();
        BoxText.SetActive(false);
        delaytimer = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (state == 1)
        {
            talk(TextOnStart);
        }

        if (state == 2)
        {
            talk(TextOnEnd);
        }
    }
    public void talk1()
    {
        BoxText.SetActive(true);
        state = 1;
        isDone = false;
        PlayerInput.Instance.setActive(false);
    }
    public void talk2()
    {
        BoxText.SetActive(true);
        state = 2;
        isDone = false;
    }


    public bool isDone = false;
    void talk(List<string> listText)
    {
        if (idx < listText.Count)
        {
            delaytimer -= Time.deltaTime;
            if (!isTextRunning && delaytimer < 0)
            {
                StartCoroutine(printSequence(listText[idx]));
                idx += 1;
            }
        }
        else if (!isTextRunning)
        {
            idx = 0;
            state = 0;
            BoxText.SetActive(false);
            isDone = true;
            delaytimer = 0;
            textBox.text = "";
            PlayerInput.Instance.setActive(true);
        }


    }

    bool isTextRunning = false;
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
            yield return new WaitForSeconds(0.02f);
        }
        isTextRunning = false;
        delaytimer = 2;
    }
}
