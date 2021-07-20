using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ɹ� ����, �ʱ�ȭ
abstract public partial class SkillBase : MonoBehaviour
{
    protected CharactorBase charactor      = null; // ������ CharactorBase
    public    GameObject    selectedTarget = null; // ��ų�� ������ ���� ������Ʈ

    protected virtual void Awake()
    {
        charactor = GetComponent<CharactorBase>();
        NullChecker.CheckNULL(charactor, true);
    }
}

// partial �� ����ϸ� �� Ŭ������ �������� ���� �� �� �־��.
// ��ų �Լ�
abstract public partial class SkillBase : MonoBehaviour
{
    // ����� ���� �Լ�
    abstract protected void SkillA();
    abstract protected void SkillB();

    // ��ų �ǰ� �� ���Ǵ� �Լ�
    abstract protected void OnSkillAHit(CharactorBase targetBase);
    abstract protected void OnSkillBHit(CharactorBase targetBase);
}