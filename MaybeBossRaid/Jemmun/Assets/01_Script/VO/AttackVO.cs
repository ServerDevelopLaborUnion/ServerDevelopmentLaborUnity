using System;

[Serializable]
public class AttackVO
{
    public int target;
    public int damage;

    public AttackVO(int target, int damage)
    {
        this.target = target;
        this.damage = damage;
    }
}
