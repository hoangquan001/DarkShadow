using System;
using System.Collections.Generic;
using UnityEngine;
public class XObject : MonoBehaviour
{
    public List<IXComponent> xComponents =  new List<IXComponent>();

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
        for (int i = 0; i < xComponents.Count; i++)
        {
           xComponents[i].Interface?.Fire(key, eventArgs);
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
    public void AddXComponent(XComponent xComponent)
    {
        xComponents.Add(xComponent);
        xComponent.OnAttach(this);
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