using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamplePlayer : CharactorBase
{
    public override void OnSkillHit(SkillEnum skillEnum)
    {
        base.OnSkillHit(skillEnum);
        TakeDamage(LastHitSkill.damage);
    }
}
