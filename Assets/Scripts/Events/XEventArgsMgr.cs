using System;
using System.Collections.Generic;
using UnityEngine;


public static class XEventArgsMgr
{
    private static Dictionary<Type,EventArgs> ListEventArgs = new Dictionary<Type, EventArgs>();

    static XEventArgsMgr()
    {
    }

    public static T GetEventArgs<T>()
    {
        if(ListEventArgs.ContainsKey(typeof(T)))
        {
            return (T)(object)ListEventArgs[typeof(T)];
        }
        var t = Activator.CreateInstance<T>();
        ListEventArgs.Add(typeof(T), t as EventArgs);
        return t;
    }
}