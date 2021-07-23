// ¹Ú»óºó °³¹ß

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : CharactorBase
{
    public override void OnSkillHit(SkillEnum skillEnum)
    {
        base.OnSkillHit(skillEnum);
        TakeDamage(LastHitSkill.damage);
    }
}
