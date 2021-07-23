using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Skills : SkillBase
{
    [Header("��ų ���� ��ư��")]
    [SerializeField] protected UnityEngine.UI.Button[] btnSkills = new UnityEngine.UI.Button[2];
                     protected UnityEngine.UI.Text[] txtBtnSkill = new UnityEngine.UI.Text[2];

    protected override void Awake()
    {
        base.Awake();

        if (charactor.isRemote) return;

        // ��ư �ؽ�Ʈ ������
        for (int i = 0; i < btnSkills.Length; ++i)
        {
            txtBtnSkill[i] = btnSkills[i].transform.GetChild(0).GetComponent<UnityEngine.UI.Text>();
        }

    }

    /// <summary>
    /// ������ ��ų�� ����ߴٴ� ���� ������ ���� �Լ�<br></br>
    /// ��ų�� Enum �� �Ѱ��ָ� �˴ϴ�.
    /// </summary>
    /// <param name="skillEnum">��ų�� Enum</param>
    public void Skill(SkillEnum skillEnum)
    {
        // �ڽ��� ���� �ƴϸ� �������� ����
        if (!charactor.isTurn) return;

        CharactorBase damage = selectedTarget.GetComponent<CharactorBase>();
        if (damage == null) return;
        SkillData skillData = SkillManager.instance.GetSkillData(skillEnum);
        if (this.charactor.mp < skillData.mpCost) return;
        this.charactor.mp -= skillData.mpCost;
        


        // ���ο� AttackVO �ν��Ͻ��� ����� �� �ȿ� Ÿ���� id, �������� �־� �� ���� JSON ���� ��ȯ�ؿ�.
        // �׸��� �װ� ���ο� DataVO �ȿ� payload �� �־��ݴϴ�.
        DataVO vo = new DataVO("attack", JsonUtility.ToJson(new AttackVO((int)(damage.id * charactor.atk), skillEnum)));

        SocketClient.Send(JsonUtility.ToJson(vo));

        TurnManager.instance.EndTurn();
    }

    /// <summary>
    /// ��ư�� ��ų�� �����ִ� �Լ�
    /// </summary>
    /// <param name="btnIndex">��ư�� �ε���. 0 ~ 1</param>
    /// <param name="function">��ư�� ������ �� ������ �Լ�</param>
    protected void SetButton(int btnIndex, string skillName, UnityEngine.Events.UnityAction function)
    {
        btnSkills[btnIndex].onClick.AddListener(function);
        txtBtnSkill[btnIndex].text = skillName;
    }
}
