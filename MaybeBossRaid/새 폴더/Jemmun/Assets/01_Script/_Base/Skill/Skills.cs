using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 스킬의 정보를 담고 있는 클레스
// UI 출력을 위해 만들었어요.
public class SkillData
{
    public readonly string name;
    public readonly string info;
    public readonly int mpCost;

    public SkillData(string name, string info, int mpCost)
    {
        this.name = name;
        this.info = info;
        this.mpCost = mpCost;
    }
}


abstract public class Skills : SkillBase
{
    // 본인의 스킬 정보를 담아 둘 예정인 사전
    protected Dictionary<SkillEnum, SkillData> skillData = new Dictionary<SkillEnum, SkillData>();

    [Header("스킬 시전 버튼들")]
    [SerializeField] protected UnityEngine.UI.Button[] btnSkills = new UnityEngine.UI.Button[2];

    protected override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// 본인의 스킬 정보를 담아 두는 사전을 초기화하는 함수
    /// </summary>
    /// <param name="name">스킬의 이름</param>
    /// <param name="info">스킬 설명</param>
    /// <param name="mpCost">MP 소모량</param>
    /// <param name="skillEnum">스킬의 Enum</param>
    protected void SetSkillData(string name, string info, int mpCost, SkillEnum skillEnum)
    {
        if (skillData.ContainsKey(skillEnum))
        {
            Debug.LogError($"추가하려고 하는 스킬이 이미 등록되어 있습니다.\r\nKey: {skillEnum}, Name: {name}");
        }
        else
        {
            skillData.Add(skillEnum, new SkillData(name, info, mpCost));
        }
    }

    /// <summary>
    /// 버튼에 스킬을 묶어주는 함수
    /// </summary>
    /// <param name="btnIndex">버튼의 인덱스. 0 ~ 1</param>
    /// <param name="function">버튼을 눌렀을 때 실행할 함수</param>
    protected void SetButton(int btnIndex, UnityEngine.Events.UnityAction function)
    {
        btnSkills[btnIndex].onClick.AddListener(function);
    }
}
