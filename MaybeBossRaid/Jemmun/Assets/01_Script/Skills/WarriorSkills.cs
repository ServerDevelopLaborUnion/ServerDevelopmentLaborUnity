// 천승현 개발

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorSkills : Skills
{
    private int defTurn = 0;
    private CharactorBase teammateBase = null;
    SkillData warriorAtk;
    SkillData warriorDef;
    GameObject[] effectObject = new GameObject[2];
    SpriteRenderer[] effectSpriteRenderer = new SpriteRenderer[2];
    [SerializeField] Sprite effectSprite = null;
    [SerializeField] ParticleSystem particleSystem = null;


    sealed protected override void Awake()
    {
        base.Awake();
        
        // 중복되는 캐릭터가 없으니 중복되는 스킬도 없을 것이라고 생각했어요.
        // 나중을 위해서 일단 전부 dictionary 에 넣어두겟슴

        warriorAtk = new SkillData("휘두르기", "검을 강하게 휘두르며 적에게 피해를 입힙니다.", 10, 20, OnSkillAHit);
        warriorDef = new SkillData("용기의 함성", "아군 한명과 함께 방어력이 강화됩니다.", 30, 0, OnSkillBHit);



        if (charactor.isRemote) return;

        SetButton(0, "휘두르기",  SkillA);
        SetButton(1, "용기의 함성", SkillB);
    }

    private void Start()
    {
        SkillManager.instance.SetSkillData(warriorAtk, SkillEnum.Wield);
        SkillManager.instance.SetSkillData(warriorDef, SkillEnum.BraveShout);
    }

    /// <summary>
    /// 디버그용 코드
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            TurnManager.instance.EndTurn();
        if (teammateBase == null) return;
        if (TurnManager.instance.turn == defTurn + 2)
        {
            StartCoroutine(ShieldEffect(teammateBase.gameObject, false));
            teammateBase.def = 1;
            this.charactor.def = 1;
            defTurn = -1;
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
        particleSystem.Play();
        Debug.Log($"{targetBase.gameObject.name}: HitA!");

        // 화려한 이팩트 뭔가 일단 무언가 부와앙앍 하는것들
    }

    protected sealed override void OnSkillBHit(CharactorBase targetBase)
    {
        StartCoroutine(ShieldEffect(targetBase.gameObject, true));
        defTurn = TurnManager.instance.turn;
        teammateBase = targetBase;
        teammateBase.def = 0.8f;
        this.charactor.def = 0.8f;

        Debug.Log($"{targetBase.gameObject.name}: HitB!");

        // 화려한 이팩트 뭔가 일단 무언가 부와앙앍 하는것들
    }

    private IEnumerator ShieldEffect(GameObject target, bool doAppear)
    {
        float colorOP;
        float size;
        if (!CheckChild(this.gameObject, "Shield"))
        {
            effectObject[0] = Instantiate(new GameObject("Shield"), this.transform);
            effectObject[0].transform.position += Vector3.back;
        }
        else
            effectObject[0].SetActive(true);
        if (!CheckChild(target.gameObject, "Shield"))
        {
            effectObject[1] = Instantiate(new GameObject("Shield"), target.transform);
            effectObject[1].transform.position += Vector3.back;
        }
        else
        {
            effectObject[1] = FindChild(target.gameObject, "Shield");
            effectObject[1].SetActive(true);
        }
        for (int i = 0; i < 2; i++)
        {
            effectObject[i].name = "Shield";
            if(effectObject[i].GetComponent<SpriteRenderer>() == null)
                effectSpriteRenderer[i] = effectObject[i].AddComponent<SpriteRenderer>();
            else
                effectSpriteRenderer[i] = effectObject[i].GetComponent<SpriteRenderer>();
            effectSpriteRenderer[i].sprite = effectSprite;
        }

        switch (doAppear)
        {
            case true:
                colorOP = 0f;
                size = 1f;
                for (int i = 0; i < 50; ++i)
                {
                    for(int j = 0; j < 2; ++j)
                    {
                        effectSpriteRenderer[j].color = new Color(1, 1, 1, colorOP += 0.008f);
                        effectObject[j].transform.localScale = new Vector2(size += 0.01f, size += 0.01f);
                    }
                    yield return new WaitForSeconds(0.01f);
                }
                break;

            case false:
                colorOP = 0.8f;
                size = 3f;
                for (int i = 0; i < 50; ++i)
                {
                    for (int j = 0; j < 2; ++j)
                    {
                        effectSpriteRenderer[j].color = new Color(1, 1, 1, colorOP -= 0.008f);
                        effectObject[j].transform.localScale = new Vector2(size -= 0.01f, size -= 0.01f);
                    }
                    yield return new WaitForSeconds(0.01f);
                }
                for (int i = 0; i < 2; ++i)
                    effectObject[i].SetActive(false);
                break;
        }
    }

    private bool CheckChild(GameObject target, string name)
    {
        for (int i = 0; i < target.transform.childCount; i++)
        {
            if (target.transform.GetChild(i).name == name)
                return true;
        }
        return false;
    }

    private GameObject FindChild(GameObject target, string name)
    {
        for (int i = 0; i < target.transform.childCount; i++)
        {
            if (target.transform.GetChild(i).name == name)
                return target.transform.GetChild(i).gameObject;
        }
        return null;
    }
}
