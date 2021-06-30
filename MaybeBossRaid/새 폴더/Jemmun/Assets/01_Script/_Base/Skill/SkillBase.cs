using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ɹ� ����, �ʱ�ȭ
abstract public partial class SkillBase : MonoBehaviour, ISkill
{
    protected CharactorBase player = null;


    #region �ʱ�ȭ, Awake()

    protected virtual void Awake()
    {
        player = GetComponent<CharactorBase>();

        NullChecker.CheckNULL(player, true);
    }

    #endregion


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