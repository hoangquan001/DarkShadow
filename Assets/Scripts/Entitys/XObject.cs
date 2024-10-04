using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class XObject : MonoBehaviour
{
    public List<IXComponent> xComponents =  new List<IXComponent>();
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

    public bool @FireEvent( EventArgs eventArgs)
    {
        string key = eventArgs.GetType().Name;
        // for (int i = 0; i < xComponents.Count; i++)
        // {
        //    xComponents[i].Interface?.Fire(key, eventArgs);
        // }
        if(events.ContainsKey(key))
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
    public virtual void Update()
    {
        for (int i = 0; i < xComponents.Count; i++)
        {
             xComponents[i].Interface?.Update();
        }
    }
    public virtual void FixedUpdate()
    {
        for (int i = 0; i < xComponents.Count; i++)
        {
             xComponents[i].Interface?.FixedUpdate();
        }
    }
    public XComponent AddXComponent(XComponent xComponent)
    {
        xComponents.Add(xComponent);
        xComponent.OnAttach(this);
        return xComponent;
    }
    public void RemoveXComponent(XComponent xComponent)
    {
        xComponents.Remove(xComponent);
    }

    public T GetXComponent<T>()
    {
        for (int i = 0; i < xComponents.Count; i++)
        {
            if(xComponents[i] is T)
            {
                return (T)(object) xComponents[i];
            }
        }
        return default;
    }

    public void FireSkill( int skill)
    {
        
    }

}