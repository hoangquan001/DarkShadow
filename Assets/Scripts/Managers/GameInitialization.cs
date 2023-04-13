using System.Collections;
using UnityEngine;


public class GameInitialization : MonoBehaviour
{
    public GameObject _UIRoot;
    // Use this for initialization
    void Start()
    {
        GameObject UIroot = GameObject.Find("UIRoot");
        if (!UIroot)
        {
            UIroot = Instantiate<GameObject>(_UIRoot);
            UIroot.name = "UIRoot";
        }
        GameObject gameManager = GameObject.Find("GameManager");
        if (!gameManager)
        {
            gameManager = new GameObject("GameManager");
            gameManager.AddComponent<SceneController>();
            gameManager.AddComponent<TimeManager>();
            DontDestroyOnLoad(gameManager);
        }
        // DontDestroyOnLoad(UIroot);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
