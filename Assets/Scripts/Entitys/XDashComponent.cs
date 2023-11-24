using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
public class XDashComponent : XComponent 
{

    private float startDash;

    public override void RegisterEvents()
    {
        base.RegisterEvents();
        Register<DashEventArgs>(OnEventAction);
    }

    private void OnEventAction(EventArgs e)
    {
        DashEventArgs args = e as DashEventArgs;
        Begin();
    }
    private void Begin()
    {
        startDash = Time.time;
    }

    public override void FixedUpdate()
    {

        base.FixedUpdate();

        if (Time.time - startDash > 0.1f)
        {
            // _entity.ApplyMove(dir)
        }
        
    }


}