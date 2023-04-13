using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class UI_MainMenu : MonoBehaviour
{
    Button start_btn;
    Button new_btn;
    Button setting_btn;
    Button quick_btn;

    [SerializeField] GameObject settingPopupPrefabs;

    // Use this for initialization
    void Awake()
    {
        start_btn = transform.Find("StartBtn").GetComponent<Button>();
        new_btn = transform.Find("NewGameBtn").GetComponent<Button>();
        setting_btn = transform.Find("SettingBtn").GetComponent<Button>();
        quick_btn = transform.Find("QuitBtn").GetComponent<Button>();

        start_btn.onClick.AddListener(delegate { OnClickStart(); });
        new_btn.onClick.AddListener(delegate { OnClickNewGame(); });
        setting_btn.onClick.AddListener(delegate { OnClickSetting(); });
        quick_btn.onClick.AddListener(delegate { OnClickQuit(); });

    }

    void OnClickStart()
    {
        UI_Loading.Instance.OnLoading();
        SceneController.Instance.PlayGame();
    }
    void OnClickNewGame()
    {
        SceneController.Instance.NewGame();
    }
    void OnClickSetting()
    {
        if (settingPopupPrefabs)
        {
            settingPopupPrefabs.SetActive(true);
        }

    }
    void OnClickQuit()
    {
        SceneController.Instance.QuitGame();
    }



}