using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 존제하는 스킬 Enum
public enum SkillEnum
{
    ExampleAtk,
    ExampleHeal,
    ExampleEND
}

// 스킬의 정보를 담고 있는 클레스
// UI 출력을 위해 만들었어요.
public class SkillData
{
    public readonly string name;
    public readonly int    damage;
    public readonly int    mpCost;

    public SkillData(string name, int damage, int mpCost)
    {
        this.name   = name;
        this.damage = damage;
        this.mpCost = mpCost;
    }
}

abstract public class Skills : SkillBase
{
    // 스킬 정보를 담아 둘 예정인 사전
    protected Dictionary<SkillEnum, SkillData> skillData = new Dictionary<SkillEnum, SkillData>();

    protected override void Awake()
    {
        base.Awake();
        InitSkillData();
    }

    // 불편함
    // 알아요
    // 아아앆
    // 나중에 json 으로 바꿔두겟슴
    private void InitSkillData()
    {
        skillData.Add(SkillEnum.ExampleAtk,  new SkillData("강려크한 공격", 154, 15));
        skillData.Add(SkillEnum.ExampleHeal, new SkillData("엄청난 힐",    -13, 15));
    }
}
