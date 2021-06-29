using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 존제하는 스킬 Enum
public enum SkillEnum
{

}

// 스킬의 정보를 담고 있는 클레스
public class SkillData
{
    public readonly string name;
    public readonly int damage;
    public readonly int mpCost;

    public SkillData(string name, int damage, int mpCost)
    {
        this.name = name;
        this.damage = damage;
        this.mpCost = mpCost;
    }
}

abstract public class Skills : SkillBase
{
    public Dictionary<SkillEnum, SkillData> skillData = new Dictionary<SkillEnum, SkillData>();


    protected override void Awake()
    {
        base.Awake();
    }
}
