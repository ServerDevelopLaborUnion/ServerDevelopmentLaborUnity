using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public partial class CharactorBase : MonoBehaviour
{
    public int hp  = 100;
    public int mp  = 100;
    public int pos = 0; // 뭐 하는 변수인지는 이번 동아리 시간에 설명 해 드리겠슴
    public int id;

    public JobList job = JobList.JOB_END;
    
    //protected string name; // 일단 주석처리 해 두겟스빈다.

    // 스텟
    // 공격력 * atk
    // 데미지 / def 로 들어갑니다.
    public float atk = 1.0f;
    public float def = 1.0f;

    public bool isDead = false; // 사망 또는 행동 불가능 상태
    public bool isRemote = false; // 본인 캐릭터인지
    public bool isTurn = false; // 본인 턴인지


    // 마지막으로 맞은 스킬 저장용
    public SkillData LastHitSkill { get; private set; }

    /// <summary>
    /// 스킬 피격시 효과 재생 + 맞은 스킬 저장
    /// </summary>
    /// <param name="skillEnum">스킬의 Enum</param>
    public virtual void OnSkillHit(SkillEnum skillEnum)
    {
        // 해당하는 스킬 피격 효과를 재생시킵니다.
        LastHitSkill = SkillManager.instance.GetSkillData(skillEnum);
        LastHitSkill.skillHitCallback(this);

        //hp -= (int)(skillData.damage / def);
    }

    /// <summary>
    /// 데미지 처리를 하는 함수
    /// </summary>
    /// <param name="damage"></param>
    protected void TakeDamage(int damage)
    {
        hp -= (int)(damage / def);
    }
}