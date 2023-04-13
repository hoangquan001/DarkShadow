using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandBookController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<GameObject> ListPages; 
    [SerializeField]  GameObject BackGround;
    bool isShow = false;
    void Awake()
    {
        //ListPages = new List<>(); 
        HiddenAll();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isShow)
            {
                HiddenAll();

            }
            else
            {
                show();
            }
            isShow = !isShow;

        }
     
    }
    void show()
    {
        BackGround.SetActive(true);
        ListPages[0].SetActive(true);
    }
    void HiddenAll()
    {
        BackGround.SetActive(false);
        foreach (GameObject page in ListPages)
        {
            page.SetActive(false);
        }
    }
}
