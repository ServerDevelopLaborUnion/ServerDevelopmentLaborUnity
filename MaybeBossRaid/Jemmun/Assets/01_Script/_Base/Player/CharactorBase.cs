using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public partial class CharactorBase : MonoBehaviour
{
    public int hp  = 100;
    public int mp  = 100;
    public int pos = 0; // �� �ϴ� ���������� �̹� ���Ƹ� �ð��� ���� �� �帮�ڽ�
    public int id;
    //protected string name; // �ϴ� �ּ�ó�� �� �ΰٽ����.

    public bool isRemote = false; // ���� ĳ��������

    /// <summary>
    /// ������ ó�� �ϴ� �Լ�
    /// </summary>
    /// <param name="damage">������, ���� ��� ������ ���Ϳ�</param>
    abstract public void OnDamage(int damage);

    /// <summary>
    /// ��ų �ǰ� ó�� �ϴ� �Լ�
    /// </summary>
    /// <param name="skillEnum">��ų�� Enum</param>
    public void OnSkillHit(SkillEnum skillEnum)
    {
        // �ش��ϴ� ��ų �ǰ� ȿ���� �����ŵ�ϴ�.
        SkillManager.instance.GetSkillData(skillEnum).skillHitCallback(this);
    }
}