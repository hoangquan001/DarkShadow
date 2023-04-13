using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    Slider _slider;

    private static SceneController instance;
    public static SceneController Instance
    {
        get
        {
            if (instance != null)
                return instance;

            instance = FindObjectOfType<SceneController>();

            if (instance != null)
                return instance;


            // Create();
            return instance;
        }
    }

    public void Awake()
    {
        // DontDestroyOnLoad(gameObject);
    }
    public void NextMap()
    {
        StartCoroutine(LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1));

    }

    public void RestartGame()
    {
        StartCoroutine(LoadSceneAsync(SceneManager.GetActiveScene().buildIndex));
    }

    public void backToStartMenu()
    {
        StartCoroutine(LoadSceneAsync(0));

    }
    public void PlayGame()
    {
        int mapidx = DataPersistanceManager.Instance.getData().currentMap;
        if (mapidx == 0)
        {
            NewGame();
        }
        else
        {
            StartCoroutine(LoadSceneAsync(mapidx));
        }

    }
    IEnumerator LoadSceneAsync(int mapidx)
    {
        UIRoot.Instance.OnLoad();

        for (float i = 0; i < 0.25f; i += 0.05f)
        {
            UI_Loading.Instance._slider.value = i * 2;
            yield return new WaitForSeconds(0.05f);
        }
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(mapidx);
        while (!asyncLoad.isDone)
        {
            UI_Loading.Instance._slider.value = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            yield return null;
        }
        UIRoot.Instance.OnFinishLoad();
    }
    public void NewGame()
    {
        DataPersistanceManager.Instance.NewGame();
        StartCoroutine(LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1));

    }
    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);

    }


}
