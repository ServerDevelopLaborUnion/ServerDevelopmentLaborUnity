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

    // ����
    // ���ݷ� * atk
    // ������ / def �� ���ϴ�.
    public float atk = 1.0f;
    public float def = 1.0f;

    // ��� �Ǵ� �ൿ �Ұ��� ����
    public bool isDead = false;
    
    public bool isRemote = false; // ���� ĳ��������

    /// <summary>
    /// ��ų �ǰ� ó�� �ϴ� �Լ�
    /// </summary>
    /// <param name="skillEnum">��ų�� Enum</param>
    public void OnSkillHit(SkillEnum skillEnum)
    {
        // �ش��ϴ� ��ų �ǰ� ȿ���� �����ŵ�ϴ�.
        SkillData skillData = SkillManager.instance.GetSkillData(skillEnum);
        skillData.skillHitCallback(this);

        hp -= (int)(skillData.damage / def);
    }
    // ����Ƽ ���ν����忡�� ������ �� �Ǵµ� ��
}