using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleSkill : Skills
{
    sealed protected override void Awake()
    {
        base.Awake();

        SetSkillData("매우 강력한 공격", "엄청나게 강력하게 공격한다", 10, SkillEnum.ExampleAtk);
        SetSkillData("매우 강력한 힐링", "완벽하게 힐을 한다.",       30, SkillEnum.ExampleHeal);

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
        // 아마 사용하지 않을 듯 하지만
    }

}
