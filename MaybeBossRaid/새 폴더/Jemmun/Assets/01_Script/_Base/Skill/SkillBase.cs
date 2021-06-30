using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ɹ� ����, �ʱ�ȭ
abstract public partial class SkillBase : MonoBehaviour, ISkill
{
    protected CharactorBase charactor      = null;
    public    GameObject    selectedTarget = null; // ��ų�� ������ ���� ������Ʈ

    protected virtual void Awake()
    {
        charactor = GetComponent<CharactorBase>();
        NullChecker.CheckNULL(charactor, true);
    }
}

// partial �� ����ϸ� �� Ŭ������ �������� ���� �� �� �־��.
// ��ų �Լ�
abstract public partial class SkillBase : MonoBehaviour, ISkill
{
    // ���Ǵ� ��ų
    abstract public void SkillA();
    abstract public void SkillB();
    abstract public void SkillC();
}