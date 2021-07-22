// 유원석 개발

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageSkills : Skills
{
    SkillData Explosion;
    SkillData PracticeExplosion;

    ParticleSystem particle = null;
    
    sealed protected override void Awake()
    {
        base.Awake();
        particle = gameObject.GetComponentInChildren<ParticleSystem>();

        // 중복되는 캐릭터가 없으니 중복되는 스킬도 없을 것이라고 생각했어요.
        // 나중을 위해서 일단 전부 dictionary 에 넣어두겟슴

        Explosion = new SkillData("폭렬 마법", "최강의 마법인 익스플로전을 사용합니다.\n\n스킬 사용 시 자신은 행동 불가 상태가 됩니다.", 10, 20, OnSkillAHit);
        PracticeExplosion = new SkillData("마법 연습", "연습을 통해 마법의 위력을 높입니다.\n\n연습 1회 당 기본 데미지에서 10 % 식 데미지가 증가합니다", 5, 0, OnSkillBHit);

        if (charactor.isRemote) return;

        SetButton(0, SkillA);
        SetButton(1, SkillB);
    }

    private void Start()
    {
        // Enum 을 바꿔주는걸 까먹었어요.
        //SkillManager.instance.SetSkillData(Explosion, SkillEnum.ExampleAtk);
        //SkillManager.instance.SetSkillData(PracticeExplosion, SkillEnum.ExampleHeal);

        SkillManager.instance.SetSkillData(Explosion, SkillEnum.Explosion);
        SkillManager.instance.SetSkillData(PracticeExplosion, SkillEnum.PracticeExplosion);
    }


    sealed protected override void SkillA()
    {
        Skill(SkillEnum.Explosion);
        this.charactor.mp -= Explosion.mpCost;
        this.charactor.atk = 0;
    }

    sealed protected override void SkillB()
    {
        Skill(SkillEnum.PracticeExplosion);
        this.charactor.mp -= PracticeExplosion.mpCost;
        this.charactor.atk += 0.1f;
    }

    protected sealed override void OnSkillAHit(CharactorBase targetBase)
    {
        Debug.Log($"{targetBase.gameObject.name}: Hit!");
        particle.transform.position = new Vector2(0, 0);
        particle.Play();
        // 화려한 이팩트 뭔가 일단 무언가 부와앙앍 하는것들
    }

    protected sealed override void OnSkillBHit(CharactorBase targetBase)
    {
        Debug.Log($"{targetBase.gameObject.name}: Hit!");
        particle.transform.position = targetBase.transform.position + transform.position;
        particle.Play();
        // 화려한 이팩트 뭔가 일단 무언가 부와앙앍 하는것들
    }
}
