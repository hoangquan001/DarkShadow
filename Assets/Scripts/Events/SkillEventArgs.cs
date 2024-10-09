using System;
using UnityEngine;


public class SkillEventArgs : EventArgs
{
    public int SkillType { get; set; }
    public float SkillValue { get; set; }
}