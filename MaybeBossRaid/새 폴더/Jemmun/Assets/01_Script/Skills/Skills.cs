using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �����ϴ� ��ų Enum
public enum SkillEnum
{
    ExampleAtk,
    ExampleHeal,
    ExampleEND
}

// ��ų�� ������ ��� �ִ� Ŭ����
// UI ����� ���� ��������.
public class SkillData
{
    public readonly string name;
    public readonly int    damage;
    public readonly int    mpCost;

    public SkillData(string name, int damage, int mpCost)
    {
        this.name   = name;
        this.damage = damage;
        this.mpCost = mpCost;
    }
}

abstract public class Skills : SkillBase
{
    // ��ų ������ ��� �� ������ ����
    protected Dictionary<SkillEnum, SkillData> skillData = new Dictionary<SkillEnum, SkillData>();

    protected override void Awake()
    {
        base.Awake();
        InitSkillData();
    }

    // ������
    // �˾ƿ�
    // �ƾƝ�
    // ���߿� json ���� �ٲ�ΰٽ�
    private void InitSkillData()
    {
        skillData.Add(SkillEnum.ExampleAtk,  new SkillData("����ũ�� ����", 154, 15));
        skillData.Add(SkillEnum.ExampleHeal, new SkillData("��û�� ��",    -13, 15));
    }
}
