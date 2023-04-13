using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;




public class UI_Setting : MonoBehaviour
{

    public List<Vector2Int> _resolutions;

    [SerializeField] GameObject FullScreen;
    [SerializeField] GameObject Volume;
    [SerializeField] GameObject Quality;

    private Toggle SreenCheck;
    private Slider VolumeSlider;
    private TMP_Dropdown QualityDropDown;
    // Use this for initialization

    private void Awake()
    {

        SreenCheck = FullScreen.GetComponent<Toggle>();
        VolumeSlider = Volume.GetComponent<Slider>();
        QualityDropDown = Quality.GetComponent<TMP_Dropdown>();

        VolumeSlider.value = DataPersistanceManager.Instance.setting_data.volume;
        SreenCheck.isOn = DataPersistanceManager.Instance.setting_data.isFullScreen;
        int[] revolution = DataPersistanceManager.Instance.setting_data.revolution;
        QualityDropDown.value = _resolutions.IndexOf(new Vector2Int(revolution[0], revolution[1]));


        VolumeSlider.onValueChanged.AddListener(
            delegate { onChangeVolume(VolumeSlider.value); });
        SreenCheck.onValueChanged.AddListener(
            delegate { onFullScreen(SreenCheck.isOn); });

        QualityDropDown.onValueChanged.AddListener(delegate { onChangeQuality(QualityDropDown.value); });

    }
    void onFullScreen(bool isOn)
    {
        Screen.fullScreen = isOn;
        DataPersistanceManager.Instance.setting_data.isFullScreen = isOn;
        DataPersistanceManager.Instance.SaveSetting();
    }
    void onChangeVolume(float value)
    {
        AudioListener.volume = value;
        DataPersistanceManager.Instance.setting_data.volume = value;
        DataPersistanceManager.Instance.SaveSetting();
    }

    public void onChangeQuality(int value)
    {
        print(_resolutions[value]);
        Screen.SetResolution(_resolutions[value].x, _resolutions[value].y, Screen.fullScreen);
        int[] _resolution = new int[2];
        _resolution[0] = _resolutions[value].x;
        _resolution[1] = _resolutions[value].y;
        DataPersistanceManager.Instance.setting_data.revolution = _resolution;
        DataPersistanceManager.Instance.SaveSetting();
    }



}
