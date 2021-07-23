// ������ ����

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

        // �ߺ��Ǵ� ĳ���Ͱ� ������ �ߺ��Ǵ� ��ų�� ���� ���̶�� �����߾��.
        // ������ ���ؼ� �ϴ� ���� dictionary �� �־�ΰٽ�

        Explosion = new SkillData("���� ����", "�ְ��� ������ �ͽ��÷����� ����մϴ�.\n\n��ų ��� �� �ڽ��� �ൿ �Ұ� ���°� �˴ϴ�.", 10, 20, OnSkillAHit);
        PracticeExplosion = new SkillData("���� ����", "������ ���� ������ ������ ���Դϴ�.\n\n���� 1ȸ �� �⺻ ���������� 10 % �� �������� �����մϴ�", 5, 0, OnSkillBHit);

        if (charactor.isRemote) return;

        SetButton(0, SkillA);
        SetButton(1, SkillB);
    }

    private void Start()
    {
        // Enum �� �ٲ��ִ°� ��Ծ����.
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
        // ȭ���� ����Ʈ ���� �ϴ� ���� �ο;Ӿ� �ϴ°͵�
    }

    protected sealed override void OnSkillBHit(CharactorBase targetBase)
    {
        Debug.Log($"{targetBase.gameObject.name}: Hit!");
        particle.transform.position = targetBase.transform.position + transform.position;
        particle.Play();
        // ȭ���� ����Ʈ ���� �ϴ� ���� �ο;Ӿ� �ϴ°͵�
    }
}
