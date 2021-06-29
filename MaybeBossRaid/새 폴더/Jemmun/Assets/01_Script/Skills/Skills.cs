using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �����ϴ� ��ų Enum
public enum SkillEnum
{

}

// ��ų�� ������ ��� �ִ� Ŭ����
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
