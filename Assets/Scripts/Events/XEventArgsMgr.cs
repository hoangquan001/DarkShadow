using System;
using System.Collections.Generic;
using UnityEngine;


public static class XEventArgsMgr
{
    private static List<EventArgs> ListEventArgs = new List<EventArgs>();

    static XEventArgsMgr()
    {
        ListEventArgs.Add(new MoveArgs());
        ListEventArgs.Add(new IdleEventArgs());
        ListEventArgs.Add(new CastSkillArgs());
        ListEventArgs.Add(new DashEventArgs());
    }

    public static T GetEventArgs<T>()
    {
        for (int i = 0; i < ListEventArgs.Count; i++)
        {
            if(ListEventArgs[i] is T)
            {
                return (T)(object)ListEventArgs[i];
            }
        }
        return default(T);
    }
}