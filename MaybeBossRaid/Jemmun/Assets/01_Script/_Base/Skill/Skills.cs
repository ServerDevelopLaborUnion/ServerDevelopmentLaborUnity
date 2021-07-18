using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 스킬의 정보를 담고 있는 클레스
// UI 출력을 위해 만들었어요.
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
    // 본인의 스킬 정보를 담아 둘 예정인 사전
    private Dictionary<SkillEnum, SkillData> skillData = new Dictionary<SkillEnum, SkillData>();
    /*
    List<string> 
    Dictionary<Key, Data>
    */


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
    /// <param name="damage">스킬의 데미지</param>
    /// <param name="skillHitCallbackFunction">스킬 피격 시 벌어지는 행동을 담은 함수</param>
    /// <param name="skillEnum">스킬의 Enum</param>
    protected void SetSkillData(string name, string info, int mpCost, int damage, SkillData.SkillHitCallback skillHitCallbackFunction, SkillEnum skillEnum)
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

    protected SkillData GetSkillData(SkillEnum skillEnum)
    {
        return skillData[skillEnum];
    }

    /// <summary>
    /// 서버로 스킬을 사용했다는 것을 보내기 위한 함수<br></br>
    /// 스킬의 Enum 만 넘겨주면 됩니다.
    /// </summary>
    /// <param name="skillEnum">스킬의 Enum</param>
    public void Skill(SkillEnum skillEnum)
    {
        IDamageable damage = selectedTarget.GetComponent<IDamageable>();
        //Rigidbody2D damage = selectedTarget.GetComponent<Rigidbody2D>();
        if (damage != null) return;

        SkillData skillData = GetSkillData(skillEnum);
        this.charactor.mp -= skillData.mpCost;

        // 새로운 AttackVO 인스턴스를 만들고 그 안에 타깃의 id, 데미지를 넣어 준 다음 JSON 으로 변환해요.
        // 그리고 그걸 새로운 DataVO 안에 payload 로 넣어줍니다.
        DataVO vo = new DataVO("attack", JsonUtility.ToJson(new AttackVO(damage.ID, skillEnum))); // 나아아중에 damage 대신 스킬 인덱스를 넣어줄까 라고 생각하고잇스빈다.

        SocketClient.Send(JsonUtility.ToJson(vo));
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
