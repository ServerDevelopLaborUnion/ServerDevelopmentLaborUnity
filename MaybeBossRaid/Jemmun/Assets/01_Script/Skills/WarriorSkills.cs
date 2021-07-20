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
        
        // �ߺ��Ǵ� ĳ���Ͱ� ������ �ߺ��Ǵ� ��ų�� ���� ���̶�� �����߾��.
        // ������ ���ؼ� �ϴ� ���� dictionary �� �־�ΰٽ�

        warriorAtk = new SkillData("�ֵθ���", "���� ���ϰ� �ֵθ��� ������ ���ظ� �����ϴ�.", 10, 20, OnSkillAHit);
        warriorDef = new SkillData("����� �Լ�", "�Ʊ� �Ѹ�� �Բ� ������ ��ȭ�˴ϴ�.", 30, 0, OnSkillBHit);



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

        // ȭ���� ����Ʈ ���� �ϴ� ���� �ο;Ӿ� �ϴ°͵�
    }

    protected sealed override void OnSkillBHit(CharactorBase targetBase)
    {
        defTurn = TurnManager.instance.turn;
        teammateBase = targetBase;
        teammateBase.def = 0.8f;
        this.charactor.def = 0.8f;

        Debug.Log($"{targetBase.gameObject.name}: HitB!");

        // ȭ���� ����Ʈ ���� �ϴ� ���� �ο;Ӿ� �ϴ°͵�
    }
}
