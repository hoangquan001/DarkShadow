using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<GameObject> boxMenus = null;
    private void Awake()
    {

        hiddenAll();

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            showMenu(0);
        }

        if (PlayerCharacter.Instance)
        {
            if (PlayerCharacter.Instance.isDead())
            {
                boxMenus[1].SetActive(true);
                Time.timeScale = 0;
            }

        }
    }
    public void showMenu(int idx)
    {
        hiddenAll();
        Time.timeScale = 0;
        if (idx >= 0 && idx < boxMenus.Count)
        {
            boxMenus[idx].SetActive(true);
        }


    }
    public void hiddenAll()
    {
        Time.timeScale = 1;
        for (int i = 0; i < boxMenus.Count; i++)
        {
            boxMenus[i].SetActive(false);

        }
    }
    public void show(GameObject game)
    {

    }
    public void restartGame()
    {
        Time.timeScale = 1;
        SceneController.Instance.RestartGame();
    }
    public void quitGame()
    {
        Time.timeScale = 1;
        SceneController.Instance.QuitGame();
    }
    public void backToMenu()
    {
        Time.timeScale = 1;
        SceneController.Instance.backToStartMenu();
    }
}
