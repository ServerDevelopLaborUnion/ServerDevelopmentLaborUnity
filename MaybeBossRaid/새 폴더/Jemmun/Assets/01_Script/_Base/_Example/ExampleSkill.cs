using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleSkill : Skills
{
    sealed protected override void Awake()
    {
        base.Awake();

        SetSkillData("�ſ� ������ ����", "��û���� �����ϰ� �����Ѵ�", 10, SkillEnum.ExampleAtk);
        SetSkillData("�ſ� ������ ����", "�Ϻ��ϰ� ���� �Ѵ�.",       30, SkillEnum.ExampleHeal);

        SetButton(0, SkillA);
        SetButton(1, SkillB);
    }


    public override void SkillA()
    {
        
    }

    public override void SkillB()
    {
        
    }

    public override void SkillC()
    {
        // �Ƹ� ������� ���� �� ������
    }

}
