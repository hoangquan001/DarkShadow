using System;
using System.Collections.Generic;
using System.IO;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
enum SkillType
{
    None = -1,
    Dash,
    Attack,
    FireMagic
}
public class XSkillMgr
{
    private XEntity host;
    private Dictionary<uint, XSkillCore> skillDir = new Dictionary<uint, XSkillCore>();
    public XSkillMgr( XEntity host)
    {
        this.host = host;
        XSkillCore core1 = new XSkillCore();
        core1.ID = 1;
        XSkillCore core2 = new XSkillCore();
        core2.ID = 2;
        skillDir.Add(core1.ID, core1);
        skillDir.Add(core2.ID, core2);

    }
    public void Update()
    {
        foreach (var core1 in skillDir.Values)
        {
            core1.Update();
        }
    }
    public void AddSkill(XSkillCore core)
    {
        skillDir.Add(core.ID, core);
    }

    public XSkillCore GetSkill(uint id)
    {
        return skillDir[id];
    }

    public bool CanCastSkill(uint id)
    {
        foreach (var core1 in skillDir.Values)
        {
            if (core1.IsCastingSkill)
            {
                return false;
            }
        }
        XSkillCore xSkillCore =  GetSkill(id);
        return !xSkillCore.IsCD;
    }

    public bool CastSkill(uint id)
    {
        SkillEventArgs @event = XEventArgsMgr.GetEventArgs<SkillEventArgs>();
        @event.SkillType  = (int)id;
        host.@FireEvent(@event);
        XSkillCore xSkillCore =  GetSkill(id);
        xSkillCore.Fire();
        return true;
    }

}