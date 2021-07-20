using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    static public SkillManager instance = null;

    // 현제 게임에 사용되는 모든 스킬들을 담은 사전
    private Dictionary<SkillEnum, SkillData> skillData = new Dictionary<SkillEnum, SkillData>();
    /*
    List<string> 
    Dictionary<Key, Data>
    */

    private void Awake()
    {
        instance = this;
    }



    /// <summary>
    /// 본인의 스킬 정보를 담아 두는 사전을 초기화하는 함수
    /// </summary>
    /// <param name="name">스킬의 이름</param>
    /// <param name="info">스킬 설명</param>
    /// <param name="mpCost">MP 소모량</param>
    /// <param name="damage">스킬의 데미지</param>
    /// <param name="skillHitCallbackFunction">스킬 피격 시 벌어지는 행동을 담은 함수</param>
    /// <param name="skillEnum">스킬의 Enum</param>
    public void SetSkillData(string name, string info, int mpCost, int damage, SkillData.SkillHitCallback skillHitCallbackFunction, SkillEnum skillEnum)
    {
        if (skillData.ContainsKey(skillEnum))
        {
            Debug.LogError($"추가하려고 하는 스킬이 이미 등록되어 있습니다.\r\nKey: {skillEnum}, Name: {name}");
        }
        else
        {
            skillData.Add(skillEnum, new SkillData(name, info, mpCost, damage, skillHitCallbackFunction));
        }
    }

    /// <summary>
    /// 본인의 스킬 정보를 담아 두는 사전을 초기화하는 함수
    /// </summary>
    /// <param name="skill">스킬 데이터 클레스</param>
    /// <param name="skillEnum">스킬의 Enum</param>
    public void SetSkillData(SkillData skill, SkillEnum skillEnum)
    {
        if (skillData.ContainsKey(skillEnum))
        {
            Debug.LogError($"추가하려고 하는 스킬이 이미 등록되어 있습니다.\r\nKey: {skillEnum}, Name: {skill.name}");
        }
        else
        {
            skillData.Add(skillEnum, skill);
        }
    }

    public SkillData GetSkillData(SkillEnum skillEnum)
    {
        return skillData[skillEnum];
    }

}
