using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardSkills : Skills
{
    SkillData fireBall;
    SkillData vision;

    [SerializeField]
    private GameObject fireBallPref = null;

    private Vector3 diff = Vector3.zero;
    private float rotationZ = 0f;

    private bool inv = false;

    sealed protected override void Awake()
    {
        base.Awake();

        // 중복되는 캐릭터가 없으니 중복되는 스킬도 없을 것이라고 생각했어요.
        // 나중을 위해서 일단 전부 dictionary 에 넣어두겟슴

        fireBall = new SkillData("파이어볼", "전방에 폭발하는 파이어볼을 날립니다. 이 폭발에 맞으면 화염대미지를 입습니다.", 100, 10, OnSkillAHit);
        vision = new SkillData("비전", "적의 공격을 한 번 회피합니다", 0, -10, OnSkillBHit);



        if (charactor.isRemote) return;

        SetButton(0, SkillA);
        SetButton(1, SkillB);
    }

    private void Start()
    {
        SkillManager.instance.SetSkillData(fireBall, SkillEnum.FireBall);
        SkillManager.instance.SetSkillData(vision, SkillEnum.Vision);
    }


    sealed protected override void SkillA()
    {
        Skill(SkillEnum.FireBall);
    }

    sealed protected override void SkillB()
    {
        Skill(SkillEnum.Vision);
    }


    protected sealed override void OnSkillAHit(CharactorBase targetBase)
    { 
        this.charactor.mp -= fireBall.mpCost;
        targetBase.hp -= fireBall.damage;

        GameObject fB = null;
        fB = Instantiate(fireBallPref, transform);
        fB.transform.SetParent(null);
        diff = transform.position - selectedTarget.transform.position;
        diff.Normalize();
        rotationZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        fB.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ + 90f);
        

        Debug.Log($"{targetBase.gameObject.name}: Hit!");

        // 화려한 이팩트 뭔가 일단 무언가 부와앙앍 하는것들
    }

    protected sealed override void OnSkillBHit(CharactorBase targetBase)
    {
        //1회 무적 판정
        inv = true;
        Debug.Log($"{targetBase.gameObject.name}: Hit!");

        // 화려한 이팩트 뭔가 일단 무언가 부와앙앍 하는것들
    }

    public bool GetInv()
    {
        return inv;
    }

    public void SetInv(bool setInv)
    {
        inv = setInv;
    }
}
