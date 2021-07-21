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

        // �ߺ��Ǵ� ĳ���Ͱ� ������ �ߺ��Ǵ� ��ų�� ���� ���̶�� �����߾��.
        // ������ ���ؼ� �ϴ� ���� dictionary �� �־�ΰٽ�

        fireBall = new SkillData("���̾", "���濡 �����ϴ� ���̾�� �����ϴ�. �� ���߿� ������ ȭ��������� �Խ��ϴ�.", 100, 10, OnSkillAHit);
        vision = new SkillData("����", "���� ������ �� �� ȸ���մϴ�", 0, -10, OnSkillBHit);



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

        // ȭ���� ����Ʈ ���� �ϴ� ���� �ο;Ӿ� �ϴ°͵�
    }

    protected sealed override void OnSkillBHit(CharactorBase targetBase)
    {
        //1ȸ ���� ����
        inv = true;
        Debug.Log($"{targetBase.gameObject.name}: Hit!");

        // ȭ���� ����Ʈ ���� �ϴ� ���� �ο;Ӿ� �ϴ°͵�
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
