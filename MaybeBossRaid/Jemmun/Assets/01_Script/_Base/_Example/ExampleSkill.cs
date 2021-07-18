using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleSkill : Skills
{
    sealed protected override void Awake()
    {
        base.Awake();

        // �ߺ��Ǵ� ĳ���Ͱ� ������ �ߺ��Ǵ� ��ų�� ���� ���̶�� �����߾��.
        // ������ ���ؼ� �ϴ� ���� dictionary �� �־�ΰٽ�
        SetSkillData("�ſ� ������ ����", "��û���� �����ϰ� �����Ѵ�", 10, 20, OnSkillAHit, SkillEnum.ExampleAtk);
        SetSkillData("�ſ� ������ ����", "�Ϻ��ϰ� ���� �Ѵ�",        30, -10, OnSkillBHit, SkillEnum.ExampleHeal);

        if (charactor.isRemote) return;

        SetButton(0, SkillA);
        SetButton(1, SkillB);
    }


    sealed protected override void SkillA()
    {
        Skill(SkillEnum.ExampleAtk);
    }

    sealed protected override void SkillB()
    {
        Skill(SkillEnum.ExampleHeal);
    }

    protected sealed override void OnSkillAHit()
    {
        // �ǰݹ��� ��� �������� ó���ؾ� �ϴµ���...
    }

    protected sealed override void OnSkillBHit()
    {
        // �ǰݹ��� ��� �������� ó���ؾ� �ϴµ���...
    }
}
