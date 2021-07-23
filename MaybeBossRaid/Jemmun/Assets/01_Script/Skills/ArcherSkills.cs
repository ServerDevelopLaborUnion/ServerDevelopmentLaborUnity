// 박상빈 개발

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherSkills : Skills
{
    SkillData quickShot;
    SkillData powerShot;

    [SerializeField]
    private GameObject arrowPrefab = null;
    [SerializeField]
    private GameObject effectPrefab = null;

    private bool isFire = false;
    private bool isTouch = false;
    private float rotationZ = 0f;
    private Vector3 diff = Vector3.zero;

    sealed protected override void Awake()
    {
        base.Awake();

        // 중복되는 캐릭터가 없으니 중복되는 스킬도 없을 것이라고 생각했어요.
        // 나중을 위해서 일단 전부 dictionary 에 넣어두겟슴

        quickShot = new SkillData("속사", "빠르게 세 발을 연달아 쏜다", 15, 5, OnSkillAHit);
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
        if (isFire) return;
        SpawningArrow();
        Skill(SkillEnum.FastShoot);

    }
    private IEnumerator QuickShot(CharactorBase targetBase)
    {
        isFire = true;
        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < 2; i++)
        {
            SpawningArrow();
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
        StartCoroutine(SpawningEffect());
        //TurnManager.instance.EndTurn();
        Debug.Log($"{targetBase.gameObject.name}: Hit!");

        // 화려한 이팩트 뭔가 일단 무언가 부와앙앍 하는것들
    }
    private void SpawningArrow()
    {
        GameObject arrow = null;
        arrow = Instantiate(arrowPrefab, transform);
        arrow.transform.SetParent(null);
        diff = transform.position - selectedTarget.transform.position;
        diff.Normalize();
        rotationZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        arrow.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ + 90f);
    }
    private IEnumerator SpawningEffect()
    {
        GameObject effect = null;
        effect = Instantiate(effectPrefab, transform);
        for (int i = 1; i <= 20; i++)
        {
            effect.transform.localScale = new Vector2(i / 10f, i / 10f);
            yield return new WaitForSeconds(0.1f);
        }
        effect.SetActive(false);
        SpawningArrow();
    }
}
