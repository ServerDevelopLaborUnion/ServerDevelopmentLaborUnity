using System;

[Serializable]
public class DamageVO
{
    public int id;
    public int damage;


    public DamageVO(int id, int damage)
    {
        this.id = id;
        this.damage = damage;
    }
}
