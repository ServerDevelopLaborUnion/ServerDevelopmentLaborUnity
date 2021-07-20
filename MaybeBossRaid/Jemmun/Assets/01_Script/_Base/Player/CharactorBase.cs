using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public partial class CharactorBase : MonoBehaviour
{
    public int hp  = 100;
    public int mp  = 100;
    public int pos = 0; // 뭐 하는 변수인지는 이번 동아리 시간에 설명 해 드리겠슴
    public int id;
    //protected string name; // 일단 주석처리 해 두겟스빈다.

    // 스텟
    // 공격력 * atk
    // 데미지 / def 로 들어갑니다.
    public float atk = 1.0f;
    public float def = 1.0f;

    // 사망 또는 행동 불가능 상태
    public bool isDead = false;
    
    public bool isRemote = false; // 본인 캐릭터인지

    /// <summary>
    /// 스킬 피격 처리 하는 함수
    /// </summary>
    /// <param name="skillEnum">스킬의 Enum</param>
    public void OnSkillHit(SkillEnum skillEnum)
    {
        // 해당하는 스킬 피격 효과를 재생시킵니다.
        SkillData skillData = SkillManager.instance.GetSkillData(skillEnum);
        skillData.skillHitCallback(this);

        hp -= (int)(skillData.damage / def);
    }
    // 유니티 메인스레드에서 실행이 안 되는듯 함
}