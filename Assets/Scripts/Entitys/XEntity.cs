using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
public class XEntity : XObject
{
    private XSkillMgr _skillMgr;
    
    public XSkillMgr SkillMgr
    {
        get
        {
            return _skillMgr;
        }
    }
    [SerializeField] private XAttributes Attributes;
    public Vector2 MoveDir = Vector2.zero;
    public override void OnCreate()
    {
        _skillMgr = new XSkillMgr();
    }
    public XAttributes getAttributes()
    {
        return Attributes;
    }

    public override void Update()
    {
        base.Update();
        _skillMgr.Update();
    }

    public void ApplyMove(Vector2 dir)
    {
        MoveDir = dir;
    }

    
    
}