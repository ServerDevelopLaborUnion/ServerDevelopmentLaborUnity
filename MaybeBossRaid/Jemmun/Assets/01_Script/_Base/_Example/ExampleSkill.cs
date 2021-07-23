using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleSkill : Skills
{
    SkillData exampleAtk;
    SkillData exampleHeal;


    sealed protected override void Awake()
    {
        base.Awake();

        // �ߺ��Ǵ� ĳ���Ͱ� ������ �ߺ��Ǵ� ��ų�� ���� ���̶�� �����߾��.
        // ������ ���ؼ� �ϴ� ���� dictionary �� �־�ΰٽ�

        exampleAtk  = new SkillData("�ſ� ������ ����", "��û���� �����ϰ� �����Ѵ�", 10, 20, OnSkillAHit);
        exampleHeal = new SkillData("�ſ� ������ ����", "�Ϻ��ϰ� ���� �Ѵ�", 30, -10, OnSkillBHit);



        if (charactor.isRemote) return;

        SetButton(0, "�ſ� ������ ����", SkillA);
        SetButton(1, "�ſ� ������ ����", SkillB);
    }

    private void Start()
    {
        SkillManager.instance.SetSkillData(exampleAtk, SkillEnum.ExampleAtk);
        SkillManager.instance.SetSkillData(exampleHeal, SkillEnum.ExampleHeal);
    }


    sealed protected override void SkillA()
    {
        Skill(SkillEnum.ExampleAtk);
        this.charactor.mp -= exampleAtk.mpCost;
    }

    sealed protected override void SkillB()
    {
        Skill(SkillEnum.ExampleHeal);
        this.charactor.mp -= exampleAtk.mpCost;
    }

    // 
    protected sealed override void OnSkillAHit(CharactorBase targetBase)
    {
        Debug.Log($"{targetBase.gameObject.name}: Hit!");

        // ȭ���� ����Ʈ ���� �ϴ� ���� �ο;Ӿ� �ϴ°͵�
    }

    protected sealed override void OnSkillBHit(CharactorBase targetBase)
    {
        Debug.Log($"{targetBase.gameObject.name}: Hit!");

        // ȭ���� ����Ʈ ���� �ϴ� ���� �ο;Ӿ� �ϴ°͵�
    }
}
