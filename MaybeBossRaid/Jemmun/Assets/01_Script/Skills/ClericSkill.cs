// �̼��� ����

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClericSkill : Skills
{
    SkillData Heal;
    SkillData Reinforce;


    sealed protected override void Awake()
    {
        base.Awake();

        // �ߺ��Ǵ� ĳ���Ͱ� ������ �ߺ��Ǵ� ��ų�� ���� ���̶�� �����߾��.
        // ������ ���ؼ� �ϴ� ���� dictionary �� �־�ΰٽ�

        Heal = new SkillData("��", "��� �Ʊ� ü���� ȸ��", 30, -10, OnSkillAHit);
        Reinforce = new SkillData("��ȭ", "�Ʊ��� ���ݷ��� ����", 40, 20, OnSkillBHit);



        if (charactor.isRemote) return;

        SetButton(0, SkillA);
        SetButton(1, SkillB);
    }

    private void Start()
    {
        SkillManager.instance.SetSkillData(Heal, SkillEnum.Heal);
        SkillManager.instance.SetSkillData(Reinforce, SkillEnum.Reinforce);
    }


    sealed protected override void SkillA()
    {
        Skill(SkillEnum.Heal);
    }

    sealed protected override void SkillB()
    {
        Skill(SkillEnum.Reinforce);
    }

    // 
    protected sealed override void OnSkillAHit(CharactorBase targetBase)
    {
        if (targetBase.hp + Heal.damage >= 100)
        {
            targetBase.hp = 100;
        }
        else
        {
            targetBase.hp -= Heal.damage;
        }
        this.charactor.mp -= Heal.mpCost;
        TurnManager.instance.EndTurn();
        Debug.Log($"{targetBase.gameObject.name}: Hit!");

        // ȭ���� ����Ʈ ���� �ϴ� ���� �ο;Ӿ� �ϴ°͵�
    }

    protected sealed override void OnSkillBHit(CharactorBase targetBase)
    {
        targetBase.atk += Reinforce.damage;
        this.charactor.mp -= Reinforce.mpCost;
        TurnManager.instance.EndTurn();
        Debug.Log($"{targetBase.gameObject.name}: Hit!");

        // ȭ���� ����Ʈ ���� �ϴ� ���� �ο;Ӿ� �ϴ°͵�
    }
}
