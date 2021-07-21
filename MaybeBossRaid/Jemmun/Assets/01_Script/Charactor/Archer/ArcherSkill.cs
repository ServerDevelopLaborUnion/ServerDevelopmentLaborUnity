using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherSkill : Skills
{
    SkillData quickShot;
    SkillData powerShot;

    private bool isFire = false;

    sealed protected override void Awake()
    {
        base.Awake();

        // 중복되는 캐릭터가 없으니 중복되는 스킬도 없을 것이라고 생각했어요.
        // 나중을 위해서 일단 전부 dictionary 에 넣어두겟슴

        quickShot  = new SkillData("속사", "빠르게 세 발을 연달아 쏜다",15, 5, OnSkillAHit);
        powerShot = new SkillData("강사", "강력한 한 발을 발사한다", 30, 30, OnSkillBHit);

        if (charactor.isRemote) return;

        SetButton(0, SkillA);
        SetButton(1, SkillB);

    }

    private void Start()
    {
        SkillManager.instance.SetSkillData(quickShot, SkillEnum.FastShoot);
        SkillManager.instance.SetSkillData(powerShot, SkillEnum.StrongShoot);
    }

    sealed protected override void SkillA()
    {
        if(isFire)return;
        Skill(SkillEnum.FastShoot);

    }
    private IEnumerator QuickShot(CharactorBase targetBase){
        isFire = true;
        yield return new WaitForSeconds(0.2f);
        for(int i = 0 ; i<2 ; i++){
            targetBase.hp -= quickShot.damage;
            yield return new WaitForSeconds(0.2f);
        }

        isFire = false;
    }
    sealed protected override void SkillB()
    {
        Skill(SkillEnum.StrongShoot);
    }
    
    protected sealed override void OnSkillAHit(CharactorBase targetBase)
    {
        StartCoroutine(QuickShot(targetBase));
        //TurnManager.instance.EndTurn();
        Debug.Log($"{targetBase.gameObject.name}: Hit!");

        // 화려한 이팩트 뭔가 일단 무언가 부와앙앍 하는것들
    }

    protected sealed override void OnSkillBHit(CharactorBase targetBase)
    {
        //TurnManager.instance.EndTurn();
        Debug.Log($"{targetBase.gameObject.name}: Hit!");

        // 화려한 이팩트 뭔가 일단 무언가 부와앙앍 하는것들
    }
}
