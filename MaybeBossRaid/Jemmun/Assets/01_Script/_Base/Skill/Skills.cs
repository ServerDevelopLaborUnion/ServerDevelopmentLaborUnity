using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ų�� ������ ��� �ִ� Ŭ����
// UI ����� ���� ��������.
public class SkillData
{
    public delegate void SkillHitCallback();

    public readonly string name;
    public readonly string info;
    public readonly int mpCost;
    public readonly int damage;
    public readonly SkillHitCallback skillHitCallback;


    public SkillData(string name, string info, int mpCost, int damage, SkillHitCallback skillHitCallback)
    {
        this.name = name;
        this.info = info;
        this.mpCost = mpCost;
        this.damage = damage;
        this.skillHitCallback = skillHitCallback;
    }
}


abstract public class Skills : SkillBase
{
    // ������ ��ų ������ ��� �� ������ ����
    private Dictionary<SkillEnum, SkillData> skillData = new Dictionary<SkillEnum, SkillData>();
    /*
    List<string> 
    Dictionary<Key, Data>
    */


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
    /// <param name="damage">��ų�� ������</param>
    /// <param name="skillHitCallbackFunction">��ų �ǰ� �� �������� �ൿ�� ���� �Լ�</param>
    /// <param name="skillEnum">��ų�� Enum</param>
    protected void SetSkillData(string name, string info, int mpCost, int damage, SkillData.SkillHitCallback skillHitCallbackFunction, SkillEnum skillEnum)
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

    protected SkillData GetSkillData(SkillEnum skillEnum)
    {
        return skillData[skillEnum];
    }

    /// <summary>
    /// ������ ��ų�� ����ߴٴ� ���� ������ ���� �Լ�<br></br>
    /// ��ų�� Enum �� �Ѱ��ָ� �˴ϴ�.
    /// </summary>
    /// <param name="skillEnum">��ų�� Enum</param>
    public void Skill(SkillEnum skillEnum)
    {
        IDamageable damage = selectedTarget.GetComponent<IDamageable>();
        //Rigidbody2D damage = selectedTarget.GetComponent<Rigidbody2D>();
        if (damage != null) return;

        SkillData skillData = GetSkillData(skillEnum);
        this.charactor.mp -= skillData.mpCost;

        // ���ο� AttackVO �ν��Ͻ��� ����� �� �ȿ� Ÿ���� id, �������� �־� �� ���� JSON ���� ��ȯ�ؿ�.
        // �׸��� �װ� ���ο� DataVO �ȿ� payload �� �־��ݴϴ�.
        DataVO vo = new DataVO("attack", JsonUtility.ToJson(new AttackVO(damage.ID, skillEnum))); // ���ƾ��߿� damage ��� ��ų �ε����� �־��ٱ� ��� �����ϰ��ս����.

        SocketClient.Send(JsonUtility.ToJson(vo));
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
