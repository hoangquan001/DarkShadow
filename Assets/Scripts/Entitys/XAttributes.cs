using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AttributeType
{
    None = -1,
    SpeedRun,
    SpeedDash,
    SpeedJump,
    SpeedFall,
    curHP,
    MaxHP,
    curMP,
    MaxMP,
    Damage,
    Defense
};
public class XAttributes
{
    Dictionary<AttributeType, float> attributes = new Dictionary<AttributeType, float>();

    public XAttributes()
    {

    }

    public float GetAttributeValue(AttributeType type)
    {
        if (!attributes.ContainsKey(type))
        {
            return 0;
        }
        return attributes[type];
    }

    public void SetAttributeValue(AttributeType type, float value)
    {
        attributes[type] = value;
    }

    public void AddAttributeValue(AttributeType type, float value)
    {
        if (!attributes.ContainsKey(type))
        {
            return;
        }
        attributes[type] += value;
    }

}
