using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_GameHUD : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject HealthBar;
    public GameObject EnergyBar;
    public GameObject Score;


    private static UI_GameHUD instance;
    public static UI_GameHUD Instance
    {
        get
        {
            if (instance != null)
                return instance;

            instance = FindObjectOfType<UI_GameHUD>();

            if (instance != null)
                return instance;

            GameObject go = new GameObject();
            go.AddComponent<UI_GameHUD>();
            instance = go.GetComponent<UI_GameHUD>();
            return instance;
        }
    }


    private void Start()
    {
        instance = this;
        // transform.Find("HealthBar");


    }
    private void Awake()
    {
        instance = this;

    }
    public void setHealthValue(float value)
    {
        if (HealthBar)
            HealthBar.GetComponent<Slider>().value = value;
    }
    public void setMaxHealth(float val)
    {
        if (HealthBar)
            HealthBar.GetComponent<Slider>().maxValue = val;
    }
    public void setMaxMana(float val)
    {
        if (EnergyBar)
            EnergyBar.GetComponent<Slider>().maxValue = val;
    }

    public void setScore(int score)
    {
        if (Score)

            Score.GetComponent<Text>().text = score.ToString();

    }
    public void setEnergyValue(float value)
    {
        if (EnergyBar)

            EnergyBar.GetComponent<Slider>().value = value;

    }


}
