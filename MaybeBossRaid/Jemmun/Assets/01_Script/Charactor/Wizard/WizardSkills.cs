using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardSkills : Skills
{
    SkillData fireBall;
    SkillData vision;


    sealed protected override void Awake()
    {
        base.Awake();

        // �ߺ��Ǵ� ĳ���Ͱ� ������ �ߺ��Ǵ� ��ų�� ���� ���̶�� �����߾��.
        // ������ ���ؼ� �ϴ� ���� dictionary �� �־�ΰٽ�

        fireBall = new SkillData("���̾", "���濡 �����ϴ� ���̾�� �����ϴ�. �� ���߿� ������ ȭ��������� �Խ��ϴ�.", 100, 10, OnSkillAHit);
        vision = new SkillData("����", "���� ������ �� �� ȸ���մϴ�", 0, -10, OnSkillBHit);



        if (charactor.isRemote) return;

        SetButton(0, SkillA);
        SetButton(1, SkillB);
    }

    private void Start()
    {
        SkillManager.instance.SetSkillData(fireBall, SkillEnum.FireBall);
        SkillManager.instance.SetSkillData(vision, SkillEnum.Vision);
    }


    sealed protected override void SkillA()
    {
        Skill(SkillEnum.FireBall);
    }

    sealed protected override void SkillB()
    {
        Skill(SkillEnum.Vision);
    }


    protected sealed override void OnSkillAHit(CharactorBase targetBase)
    {
        targetBase.hp -= fireBall.damage;
        this.charactor.mp -= fireBall.mpCost;

        Debug.Log($"{targetBase.gameObject.name}: Hit!");

        // ȭ���� ����Ʈ ���� �ϴ� ���� �ο;Ӿ� �ϴ°͵�
    }

    protected sealed override void OnSkillBHit(CharactorBase targetBase)
    {
        targetBase.hp -= fireBall.damage;
        this.charactor.mp -= fireBall.mpCost;

        Debug.Log($"{targetBase.gameObject.name}: Hit!");

        // ȭ���� ����Ʈ ���� �ϴ� ���� �ο;Ӿ� �ϴ°͵�
    }
}
