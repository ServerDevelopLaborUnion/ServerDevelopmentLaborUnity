using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherSkill : Skills
{
    SkillData quickShot;
    SkillData powerShot;


    sealed protected override void Awake()
    {
        base.Awake();

        // �ߺ��Ǵ� ĳ���Ͱ� ������ �ߺ��Ǵ� ��ų�� ���� ���̶�� �����߾��.
        // ������ ���ؼ� �ϴ� ���� dictionary �� �־�ΰٽ�

        quickShot  = new SkillData("�ӻ�", "������ �� ���� ���޾� ���", 10, 15, OnSkillAHit);
        powerShot = new SkillData("����", "������ �� ���� �߻��Ѵ�", 30, 30, OnSkillBHit);



        if (charactor.isRemote) return;

        SetButton(0, SkillA);
        SetButton(1, SkillB);
    }

    private void Start()
    {
        SkillManager.instance.SetSkillData(quickShot, SkillEnum.ExampleAtk);
        SkillManager.instance.SetSkillData(powerShot, SkillEnum.ExampleHeal);
    }


    sealed protected override void SkillA()
    {
        Skill(SkillEnum.ExampleAtk);
    }

    sealed protected override void SkillB()
    {
        Skill(SkillEnum.ExampleHeal);
    }

    
    protected sealed override void OnSkillAHit(CharactorBase targetBase)
    {
        targetBase.hp -= quickShot.damage;
        this.charactor.mp -= quickShot.mpCost;
        TurnManager.instance.EndTurn();
        Debug.Log($"{targetBase.gameObject.name}: Hit!");

        // ȭ���� ����Ʈ ���� �ϴ� ���� �ο;Ӿ� �ϴ°͵�
    }

    protected sealed override void OnSkillBHit(CharactorBase targetBase)
    {
        targetBase.hp -= powerShot.damage;
        this.charactor.mp -= powerShot.mpCost;
        TurnManager.instance.EndTurn();
        Debug.Log($"{targetBase.gameObject.name}: Hit!");

        // ȭ���� ����Ʈ ���� �ϴ� ���� �ο;Ӿ� �ϴ°͵�
    }
}
