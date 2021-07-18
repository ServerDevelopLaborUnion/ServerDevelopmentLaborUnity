using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleSkill : Skills
{
    sealed protected override void Awake()
    {
        base.Awake();

        // 중복되는 캐릭터가 없으니 중복되는 스킬도 없을 것이라고 생각했어요.
        // 나중을 위해서 일단 전부 dictionary 에 넣어두겟슴
        SetSkillData("매우 강력한 공격", "엄청나게 강력하게 공격한다", 10, 20, OnSkillAHit, SkillEnum.ExampleAtk);
        SetSkillData("매우 강력한 힐링", "완벽하게 힐을 한다",        30, -10, OnSkillBHit, SkillEnum.ExampleHeal);

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
        // 피격받은 상대 기준으로 처리해야 하는데요...
    }

    protected sealed override void OnSkillBHit()
    {
        // 피격받은 상대 기준으로 처리해야 하는데요...
    }
}
