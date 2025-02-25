using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class XObject : MonoBehaviour
{
    private Dictionary<string, UnityAction<EventArgs>> events = new Dictionary<string, UnityAction<EventArgs>>();
    public virtual void Awake()
    {
        OnCreate();
        InitComponent();
    }
    public virtual void InitComponent()
    {

    }
    public virtual void OnCreate()
    {

    }

    public bool FireEvent(EventArgs eventArgs)
    {
        string key = eventArgs.GetType().Name;
        // for (int i = 0; i < xComponents.Count; i++)
        // {
        //    xComponents[i].Interface?.Fire(key, eventArgs);
        // }
        if (events.ContainsKey(key))
        {
            events[key].Invoke(eventArgs);
        }
        return true;
    }
    public bool RegisterEvent<T>(UnityAction<EventArgs> action)
    {
        string key = typeof(T).Name;
        if (!events.ContainsKey(key))
        {
            events.Add(key, action);
        }
        else
        {
            events[key] += action;
        }
        return true;
    }



  

}