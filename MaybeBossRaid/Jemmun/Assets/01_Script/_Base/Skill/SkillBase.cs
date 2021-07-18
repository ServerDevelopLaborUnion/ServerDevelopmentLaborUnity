using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ɹ� ����, �ʱ�ȭ
abstract public partial class SkillBase : MonoBehaviour
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
abstract public partial class SkillBase : MonoBehaviour
{
    // ���Ǵ� ��ų
    abstract protected void SkillA();
    abstract protected void SkillB();

    // ��ų �ǰ� �� ���Ǵ� �Լ�
    abstract protected void OnSkillAHit();
    abstract protected void OnSkillBHit();
}