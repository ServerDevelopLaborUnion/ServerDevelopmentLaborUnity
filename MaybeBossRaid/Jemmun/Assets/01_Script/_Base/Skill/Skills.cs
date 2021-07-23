using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Skills : SkillBase
{
    [Header("스킬 시전 버튼들")]
    [SerializeField] protected UnityEngine.UI.Button[] btnSkills = new UnityEngine.UI.Button[2];
                     protected UnityEngine.UI.Text[] txtBtnSkill = new UnityEngine.UI.Text[2];

    protected override void Awake()
    {
        base.Awake();

        if (charactor.isRemote) return;

        // 버튼 텍스트 가져옴
        for (int i = 0; i < btnSkills.Length; ++i)
        {
            txtBtnSkill[i] = btnSkills[i].transform.GetChild(0).GetComponent<UnityEngine.UI.Text>();
        }

    }

    /// <summary>
    /// 서버로 스킬을 사용했다는 것을 보내기 위한 함수<br></br>
    /// 스킬의 Enum 만 넘겨주면 됩니다.
    /// </summary>
    /// <param name="skillEnum">스킬의 Enum</param>
    public void Skill(SkillEnum skillEnum)
    {
        // 자신의 턴이 아니면 실행하지 않음
        if (!charactor.isTurn) return;

        CharactorBase damage = selectedTarget.GetComponent<CharactorBase>();
        if (damage == null) return;
        SkillData skillData = SkillManager.instance.GetSkillData(skillEnum);
        if (this.charactor.mp < skillData.mpCost) return;
        this.charactor.mp -= skillData.mpCost;
        


        // 새로운 AttackVO 인스턴스를 만들고 그 안에 타깃의 id, 데미지를 넣어 준 다음 JSON 으로 변환해요.
        // 그리고 그걸 새로운 DataVO 안에 payload 로 넣어줍니다.
        DataVO vo = new DataVO("attack", JsonUtility.ToJson(new AttackVO((int)(damage.id * charactor.atk), skillEnum)));

        SocketClient.Send(JsonUtility.ToJson(vo));

        TurnManager.instance.EndTurn();
    }

    /// <summary>
    /// 버튼에 스킬을 묶어주는 함수
    /// </summary>
    /// <param name="btnIndex">버튼의 인덱스. 0 ~ 1</param>
    /// <param name="function">버튼을 눌렀을 때 실행할 함수</param>
    protected void SetButton(int btnIndex, string skillName, UnityEngine.Events.UnityAction function)
    {
        btnSkills[btnIndex].onClick.AddListener(function);
        txtBtnSkill[btnIndex].text = skillName;
    }
}
