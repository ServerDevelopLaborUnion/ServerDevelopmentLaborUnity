using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 맴버 변수, 초기화
abstract public partial class SkillBase : MonoBehaviour, ISkill
{
    protected CharactorBase charactor      = null;
    public    GameObject    selectedTarget = null; // 스킬을 시전할 게임 오브젝트

    protected virtual void Awake()
    {
        charactor = GetComponent<CharactorBase>();
        NullChecker.CheckNULL(charactor, true);
    }
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