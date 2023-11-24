using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
public interface IXComponent
{    
    public IXComponent Interface    { get; }
    public  void Update();
    public  void FixedUpdate();
    public void Fire(string Event, EventArgs args);
}
public class XComponent: IXComponent
{
    public IXComponent Interface    
    {
        get
        {
            if(!_enable)
            {
                return null;
            }
            return this;
        }
      
    }
    private bool _enable  = true;
    private Dictionary<string, UnityAction<EventArgs>> events = new Dictionary<string, UnityAction<EventArgs>>();
    public bool Enable 
    {
        set
        {
            _enable = false;
        }
        get
        {
            return true;
        }
    }
    public XObject host;
    public XEntity _entity
    {
        get
        {
            return host as XEntity;
        }
    }
    public virtual void Init() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public virtual void OnEnable() { }
    public virtual void OnDisable() { }
    public virtual void OnDestroy() { }
    public virtual void OnAttach(XObject host) 
    {
        this.host = host;
        RegisterEvents();
    }
    public virtual void OnDetach() { }
    protected void Register<T>(UnityAction<EventArgs> action)
    {
        string key= typeof(T).Name;
        if (!events.ContainsKey(key))
        {
            events.Add(key, action);
        }
    }
    public void Fire(string Event, EventArgs args)
    {
        if (events.ContainsKey(Event))
        {
            events[Event].Invoke(args);
        }
    }
    public virtual void RegisterEvents() 
    {

        
    }

    public virtual void UnregisterEvents() 
    { 

    }

    public static T CreateComponent<T>() where T: new()
    {
      return  new T();
    }

}
