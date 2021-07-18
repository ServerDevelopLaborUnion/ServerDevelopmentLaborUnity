using System;

[Serializable]
public class AttackVO
{
    public int       target;
    public SkillEnum skillEnum;

    public AttackVO(int target, SkillEnum skillEnum)
    {
        this.target    = target;
        this.skillEnum = skillEnum;
    }
}
