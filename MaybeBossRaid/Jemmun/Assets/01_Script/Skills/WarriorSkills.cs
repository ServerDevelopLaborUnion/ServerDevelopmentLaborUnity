using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorSkills : Skills
{
    private int defTurn = 0;
    private CharactorBase teammateBase = null;
    SkillData warriorAtk;
    SkillData warriorDef;


    sealed protected override void Awake()
    {
        base.Awake();
        
        // 중복되는 캐릭터가 없으니 중복되는 스킬도 없을 것이라고 생각했어요.
        // 나중을 위해서 일단 전부 dictionary 에 넣어두겟슴

        warriorAtk = new SkillData("휘두르기", "검을 강하게 휘두르며 적에게 피해를 입힙니다.", 10, 20, OnSkillAHit);
        warriorDef = new SkillData("용기의 함성", "아군 한명과 함께 방어력이 강화됩니다.", 30, 0, OnSkillBHit);



        if (charactor.isRemote) return;

        SetButton(0, SkillA);
        SetButton(1, SkillB);
    }

    private void Start()
    {
        SkillManager.instance.SetSkillData(warriorAtk, SkillEnum.Wield);
        SkillManager.instance.SetSkillData(warriorDef, SkillEnum.BraveShout);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            TurnManager.instance.EndTurn();
        if (teammateBase == null) return;
        if (TurnManager.instance.turn == defTurn + 2)
        {
            teammateBase.def = 1;
            this.charactor.def = 1;
        }
    }

    sealed protected override void SkillA()
    {
        Skill(SkillEnum.Wield);
    }

    sealed protected override void SkillB()
    {
        Skill(SkillEnum.BraveShout);
    }


    protected sealed override void OnSkillAHit(CharactorBase targetBase)
    {

        Debug.Log($"{targetBase.gameObject.name}: HitA!");

        // 화려한 이팩트 뭔가 일단 무언가 부와앙앍 하는것들
    }

    protected sealed override void OnSkillBHit(CharactorBase targetBase)
    {
        defTurn = TurnManager.instance.turn;
        teammateBase = targetBase;
        teammateBase.def = 0.8f;
        this.charactor.def = 0.8f;

        Debug.Log($"{targetBase.gameObject.name}: HitB!");

        // 화려한 이팩트 뭔가 일단 무언가 부와앙앍 하는것들
    }
}
