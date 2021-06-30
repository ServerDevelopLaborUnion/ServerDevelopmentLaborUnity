using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 맴버 변수, 초기화
abstract public partial class SkillBase : MonoBehaviour, ISkill
{
    protected CharactorBase player = null;


    #region 초기화, Awake()

    protected virtual void Awake()
    {
        player = GetComponent<CharactorBase>();

        NullChecker.CheckNULL(player, true);
    }

    #endregion


}

// partial 을 사용하면 한 클래스를 조각조각 나눠 둘 수 있어요.
// 스킬 함수
abstract public partial class SkillBase : MonoBehaviour, ISkill
{
    // 사용되는 스킬
    abstract public void SkillA();
    abstract public void SkillB();
    abstract public void SkillC();
}