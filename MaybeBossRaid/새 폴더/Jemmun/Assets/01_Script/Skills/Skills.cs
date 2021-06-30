using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ų�� ������ ��� �ִ� Ŭ����
// UI ����� ���� ��������.
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
    // ������ ��ų ������ ��� �� ������ ����
    protected Dictionary<SkillEnum, SkillData> skillData = new Dictionary<SkillEnum, SkillData>();

    [Header("��ų ���� ��ư��")]
    [SerializeField] protected UnityEngine.UI.Button[] btnSkills = new UnityEngine.UI.Button[2];

    protected override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// ������ ��ų ������ ��� �δ� ������ �ʱ�ȭ�ϴ� �Լ�
    /// </summary>
    /// <param name="name">��ų�� �̸�</param>
    /// <param name="info">��ų ����</param>
    /// <param name="mpCost">MP �Ҹ�</param>
    /// <param name="skillEnum">��ų�� Enum</param>
    protected void SetSkillData(string name, string info, int mpCost, SkillEnum skillEnum)
    {
        if (skillData.ContainsKey(skillEnum))
        {
            Debug.LogError($"�߰��Ϸ��� �ϴ� ��ų�� �̹� ��ϵǾ� �ֽ��ϴ�.\r\nKey: {skillEnum}, Name: {name}");
        }
        else
        {
            skillData.Add(skillEnum, new SkillData(name, info, mpCost));
        }
    }

    /// <summary>
    /// ��ư�� ��ų�� �����ִ� �Լ�
    /// </summary>
    /// <param name="btnIndex">��ư�� �ε���. 0 ~ 1</param>
    /// <param name="function">��ư�� ������ �� ������ �Լ�</param>
    protected void SetButton(int btnIndex, UnityEngine.Events.UnityAction function)
    {
        btnSkills[btnIndex].onClick.AddListener(function);
    }
}
