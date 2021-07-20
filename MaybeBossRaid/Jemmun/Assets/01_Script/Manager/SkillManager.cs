using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    static public SkillManager instance = null;

    // ���� ���ӿ� ���Ǵ� ��� ��ų���� ���� ����
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
    /// ������ ��ų ������ ��� �δ� ������ �ʱ�ȭ�ϴ� �Լ�
    /// </summary>
    /// <param name="name">��ų�� �̸�</param>
    /// <param name="info">��ų ����</param>
    /// <param name="mpCost">MP �Ҹ�</param>
    /// <param name="damage">��ų�� ������</param>
    /// <param name="skillHitCallbackFunction">��ų �ǰ� �� �������� �ൿ�� ���� �Լ�</param>
    /// <param name="skillEnum">��ų�� Enum</param>
    public void SetSkillData(string name, string info, int mpCost, int damage, SkillData.SkillHitCallback skillHitCallbackFunction, SkillEnum skillEnum)
    {
        if (skillData.ContainsKey(skillEnum))
        {
            Debug.LogError($"�߰��Ϸ��� �ϴ� ��ų�� �̹� ��ϵǾ� �ֽ��ϴ�.\r\nKey: {skillEnum}, Name: {name}");
        }
        else
        {
            skillData.Add(skillEnum, new SkillData(name, info, mpCost, damage, skillHitCallbackFunction));
        }
    }

    /// <summary>
    /// ������ ��ų ������ ��� �δ� ������ �ʱ�ȭ�ϴ� �Լ�
    /// </summary>
    /// <param name="skill">��ų ������ Ŭ����</param>
    /// <param name="skillEnum">��ų�� Enum</param>
    public void SetSkillData(SkillData skill, SkillEnum skillEnum)
    {
        if (skillData.ContainsKey(skillEnum))
        {
            Debug.LogError($"�߰��Ϸ��� �ϴ� ��ų�� �̹� ��ϵǾ� �ֽ��ϴ�.\r\nKey: {skillEnum}, Name: {skill.name}");
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
