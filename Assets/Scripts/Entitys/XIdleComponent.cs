using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
public class XIdleComponent : XComponent 
{

    public override void RegisterEvents()
    {
        base.RegisterEvents();
        Register<IdleEventArgs>(OnEventAction);
    }

    private void OnEventAction(EventArgs e)
    {
          _entity.ApplyMove((e as IdleEventArgs).dir);
    }
}