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
    // private Dictionary<string, UnityAction<EventArgs>> events = new Dictionary<string, UnityAction<EventArgs>>();
    public bool Enable 
    {
        set
        {
            _enable = value;
        }
        get
        {
            return _enable;
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
        host.RegisterEvent<T>(action);
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
