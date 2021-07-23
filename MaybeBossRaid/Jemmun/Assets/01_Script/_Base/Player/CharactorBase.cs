using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public partial class CharactorBase : MonoBehaviour
{
    public int hp  = 100;
    public int mp  = 100;
    public int pos = 0; // �� �ϴ� ���������� �̹� ���Ƹ� �ð��� ���� �� �帮�ڽ�
    public int id;

    public JobList job = JobList.JOB_END;
    
    //protected string name; // �ϴ� �ּ�ó�� �� �ΰٽ����.

    // ����
    // ���ݷ� * atk
    // ������ / def �� ���ϴ�.
    public float atk = 1.0f;
    public float def = 1.0f;

    public bool isDead = false; // ��� �Ǵ� �ൿ �Ұ��� ����
    public bool isRemote = false; // ���� ĳ��������
    public bool isTurn = false; // ���� ������


    // ���������� ���� ��ų �����
    public SkillData LastHitSkill { get; private set; }

    /// <summary>
    /// ��ų �ǰݽ� ȿ�� ��� + ���� ��ų ����
    /// </summary>
    /// <param name="skillEnum">��ų�� Enum</param>
    public virtual void OnSkillHit(SkillEnum skillEnum)
    {
        // �ش��ϴ� ��ų �ǰ� ȿ���� �����ŵ�ϴ�.
        LastHitSkill = SkillManager.instance.GetSkillData(skillEnum);
        LastHitSkill.skillHitCallback(this);

        //hp -= (int)(skillData.damage / def);
    }

    /// <summary>
    /// ������ ó���� �ϴ� �Լ�
    /// </summary>
    /// <param name="damage"></param>
    protected void TakeDamage(int damage)
    {
        hp -= (int)(damage / def);
    }
}