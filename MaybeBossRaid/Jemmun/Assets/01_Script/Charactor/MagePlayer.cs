// ������ ����

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagePlayer : CharactorBase
{
    public override void OnSkillHit(SkillEnum skillEnum)
    {
        base.OnSkillHit(skillEnum);
        TakeDamage(LastHitSkill.damage);
    }
}
