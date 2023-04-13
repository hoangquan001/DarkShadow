using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface DataPersister 
{
   public void SaveData(ref GameData data);
   public void LoadData(GameData data);
}

[System.Serializable]
public class GameData
{
    public int playerScore;
    public int currentMap;

    public void init()
    {
        playerScore = 0;
        currentMap = 0;
    }
}



[System.Serializable]
public class SettingData
{
    public bool isFullScreen;
    public float volume;
    public int[] revolution;


    public SettingData()
    {
        isFullScreen = true;
        volume = 0.5f;
        revolution = new int[2];
        revolution[0] = 1920;
        revolution[1] = 1080;
    }
}
