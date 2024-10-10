using System;
using UnityEngine;

public class DashEventArgs : EventArgs
{
    public float DashRange;
    public float DashSpeed;

    public AnimationClip AnimationClip;
}