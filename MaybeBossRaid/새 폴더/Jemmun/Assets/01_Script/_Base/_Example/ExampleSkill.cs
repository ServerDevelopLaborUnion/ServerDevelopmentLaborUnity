using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleSkill : Skills
{
    sealed protected override void Awake()
    {
        base.Awake();

        if (charactor.isRemote) return;   

        SetSkillData("�ſ� ������ ����", "��û���� �����ϰ� �����Ѵ�", 10, SkillEnum.ExampleAtk);
        SetSkillData("�ſ� ������ ����", "�Ϻ��ϰ� ���� �Ѵ�",        30, SkillEnum.ExampleHeal);

        SetButton(0, SkillA);
        SetButton(1, SkillB);
    }


    public override void SkillA()
    {
        IDamageable damage = selectedTarget.GetComponent<IDamageable>();
        if (damage != null) return;

        damage.OnDamage(10);
        charactor.mp -= GetSkillData(SkillEnum.ExampleAtk).mpCost;
    }

    public override void SkillB()
    {
        IDamageable damage = selectedTarget.GetComponent<IDamageable>();
        if (damage != null) return;

        damage.OnDamage(-10);
        charactor.mp -= GetSkillData(SkillEnum.ExampleHeal).mpCost;
    }

    public override void SkillC()
    {
        // �Ƹ� ������� ���� �� ������
    }

}
