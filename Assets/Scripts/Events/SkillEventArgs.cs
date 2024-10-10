using System;
using UnityEngine;


public class SkillEventArgs : EventArgs
{
    public int SkillId { get; set; }
    public float SkillValue { get; set; }
}