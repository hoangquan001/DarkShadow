using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class UIRoot : MonoBehaviour
{
    [SerializeField] GameObject gameHUD;
    [SerializeField] GameObject UIloading;
    // [SerializeField] GameObject gameHUD;
    // [SerializeField] GameObject gameHUD;
    // [SerializeField] GameObject gameHUD;
    // [SerializeField] GameObject gameHUD;
    // [SerializeField] GameObject gameHUD;
    // [SerializeField] GameObject gameHUD;
    // [SerializeField] GameObject gameHUD;
    // [SerializeField] GameObject gameHUD;

    public Dictionary<string, GameObject> listUIs;
    private static UIRoot _instance;
    public static UIRoot Instance
    {
        get
        {
            return _instance;
        }
    }
    // Use this for initialization
    void Awake()
    {
        _instance = this;
        Init();
    }

    public void SetActiveAll(bool isActive)
    {
        foreach (var item in listUIs)
        {
            item.Value.SetActive(isActive);
        }
    }

    void Init()
    {
        listUIs = new Dictionary<string, GameObject>();
        foreach (Transform tran in transform)
            listUIs[tran.name] = tran.gameObject;

        // if (!listUIs.ContainsKey("UIGameHUD") && SceneManager.GetActiveScene().name != "Start")
        // {
        //     gameHUD = Instantiate<GameObject>(gameHUD, transform);
        //     gameHUD.name = "UIGameHUD";
        //     listUIs["UIGameHUD"] = gameHUD;
        // }
        if (!listUIs.ContainsKey("UILoading"))
        {
            UIloading = Instantiate<GameObject>(UIloading, transform);
            UIloading.SetActive(false);
            UIloading.name = "UILoading";
            listUIs["UILoading"] = UIloading;
        }
    }

    // Update is called once per frame
    public void OnFinishLoad()
    {
        UI_Loading.Instance.OnDoneLoading();

    }
    public void OnLoad()
    {
        UI_Loading.Instance.OnLoading();

    }

}
