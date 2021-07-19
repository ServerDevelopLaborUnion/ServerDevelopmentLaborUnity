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

        // 중복되는 캐릭터가 없으니 중복되는 스킬도 없을 것이라고 생각했어요.
        // 나중을 위해서 일단 전부 dictionary 에 넣어두겟슴

        exampleAtk  = new SkillData("매우 강력한 공격", "엄청나게 강력하게 공격한다", 10, 20, OnSkillAHit);
        exampleHeal = new SkillData("매우 강력한 힐링", "완벽하게 힐을 한다", 30, -10, OnSkillBHit);



        if (charactor.isRemote) return;

        SetButton(0, SkillA);
        SetButton(1, SkillB);
    }

    private void Start()
    {
        SkillManager.instance.SetSkillData(exampleAtk, SkillEnum.ExampleAtk);
        SkillManager.instance.SetSkillData(exampleHeal, SkillEnum.ExampleHeal);
    }


    sealed protected override void SkillA()
    {
        Skill(SkillEnum.ExampleAtk);
    }

    sealed protected override void SkillB()
    {
        Skill(SkillEnum.ExampleHeal);
    }

    // 
    protected sealed override void OnSkillAHit(CharactorBase targetBase)
    {
        targetBase.hp -= exampleAtk.damage;
        this.charactor.mp -= exampleAtk.mpCost;

        Debug.Log($"{targetBase.gameObject.name}: Hit!");

        // 화려한 이팩트 뭔가 일단 무언가 부와앙앍 하는것들
    }

    protected sealed override void OnSkillBHit(CharactorBase targetBase)
    {
        targetBase.hp -= exampleAtk.damage;
        this.charactor.mp -= exampleAtk.mpCost;

        Debug.Log($"{targetBase.gameObject.name}: Hit!");

        // 화려한 이팩트 뭔가 일단 무언가 부와앙앍 하는것들
    }
}
