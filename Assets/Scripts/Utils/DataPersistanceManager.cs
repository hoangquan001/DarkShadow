using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class DataPersistanceManager 
{
    // Start is called before the first frame update


    bool isLoad = false;

    private static DataPersistanceManager instance;
    public static DataPersistanceManager Instance
    {
        get
        {
            if (instance == null) instance = new DataPersistanceManager();
            return instance;
        }

    }
    GameData game_data=null;
    public SettingData setting_data=null;

    DataPersistanceManager()
    {
        game_data = new GameData();
        setting_data = new SettingData();


        isLoad = false;
        instance = this;
        LoadGame();
        LoadSetting();
    }


    public void SaveGame()
    {
        string json = JsonUtility.ToJson(game_data);
        string path = Path.Combine(Application.persistentDataPath, "gamedata.json");
        File.WriteAllText(path, json);
    }


    public void LoadSetting()
    {
        string path = Path.Combine(Application.persistentDataPath, "setting.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            setting_data = JsonUtility.FromJson<SettingData>(json);
            
        }

    }


    public void SaveSetting()
    {
        string json = JsonUtility.ToJson(setting_data);
        string path = Path.Combine(Application.persistentDataPath, "setting.json");
        File.WriteAllText(path, json);
    }




    public void LoadGame()
    {
        if (isLoad ==false)
        {
            string path = Path.Combine(Application.persistentDataPath, "gamedata.json");
            if (File.Exists(path))
            {

                string json = File.ReadAllText(path);
                game_data = JsonUtility.FromJson<GameData>(json);
            }
            isLoad = true;
        }


    }

    public GameData getData()
    {
        return game_data;
    }
    void Continue()
    {

    }
    public void nextMap()
    {
        PlayerCharacter player=GameObject.FindObjectOfType<PlayerCharacter>();
        if (player != null)
        {
            player.SaveData(ref game_data);
        }
        game_data.currentMap = SceneManager.GetActiveScene().buildIndex + 1;
        SaveGame();
    }
    public void NewGame()
    {
        game_data.init();
        SaveGame();
    }
}
