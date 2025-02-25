using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class UILoading : MonoBehaviour
{
    public Slider _slider;
    private static UILoading _instance;
    private void Awake()
    {
        _instance = this;
        _slider = GetComponentInChildren<Slider>();
        _slider.value = 0;
    }

    public static UILoading Instance
    {
        get
        {
            if (!_instance)
            {
                GameObject go = GameObject.Find("UIRoot").transform.Find("UILoading").gameObject;
                if (go)
                {
                    _instance = go.GetComponent<UILoading>();
                    return _instance;
                }
            }
            return _instance;
        }
    }


    public void OnLoading()
    {
        UIRoot.Instance.SetActiveAll(false);
        gameObject.SetActive(true);
        _slider.value = 0;
    }
    public void OnDoneLoading()
    {
        // UIRoot.Instance.SetActiveAll(true);
        gameObject.SetActive(false);
    }
}
