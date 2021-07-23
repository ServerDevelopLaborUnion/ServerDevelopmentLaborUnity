// 이성현 개발

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleric : CharactorBase
{
    public override void OnSkillHit(SkillEnum skillEnum)
    {
        base.OnSkillHit(skillEnum);
        TakeDamage(LastHitSkill.damage);
    }
}
