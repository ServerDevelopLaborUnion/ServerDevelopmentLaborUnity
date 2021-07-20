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

        // �ߺ��Ǵ� ĳ���Ͱ� ������ �ߺ��Ǵ� ��ų�� ���� ���̶�� �����߾��.
        // ������ ���ؼ� �ϴ� ���� dictionary �� �־�ΰٽ�

        quickShot  = new SkillData("�ӻ�", "������ �� ���� ���޾� ���",15, 5, OnSkillAHit);
        powerShot = new SkillData("����", "������ �� ���� �߻��Ѵ�", 30, 30, OnSkillBHit);

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

        // ȭ���� ����Ʈ ���� �ϴ� ���� �ο;Ӿ� �ϴ°͵�
    }

    protected sealed override void OnSkillBHit(CharactorBase targetBase)
    {
        //TurnManager.instance.EndTurn();
        Debug.Log($"{targetBase.gameObject.name}: Hit!");

        // ȭ���� ����Ʈ ���� �ϴ� ���� �ο;Ӿ� �ϴ°͵�
    }
}
