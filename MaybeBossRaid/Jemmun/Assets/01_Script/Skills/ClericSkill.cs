// 이성현 개발

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

        // 중복되는 캐릭터가 없으니 중복되는 스킬도 없을 것이라고 생각했어요.
        // 나중을 위해서 일단 전부 dictionary 에 넣어두겟슴

        Heal = new SkillData("힐", "모든 아군 체력을 회복", 30, -10, OnSkillAHit);
        Reinforce = new SkillData("강화", "아군의 공격력을 증가", 40, 20, OnSkillBHit);



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

        // 화려한 이팩트 뭔가 일단 무언가 부와앙앍 하는것들
    }

    protected sealed override void OnSkillBHit(CharactorBase targetBase)
    {
        targetBase.atk += Reinforce.damage;
        this.charactor.mp -= Reinforce.mpCost;
        TurnManager.instance.EndTurn();
        Debug.Log($"{targetBase.gameObject.name}: Hit!");

        // 화려한 이팩트 뭔가 일단 무언가 부와앙앍 하는것들
    }
}
