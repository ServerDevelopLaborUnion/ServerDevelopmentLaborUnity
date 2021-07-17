using System;

[Serializable]
public class PlayerDataVO
{
    public int hp;
    public int mp;
    public int id;

    public PlayerDataVO(int id, int hp, int mp)
    {
        this.hp = hp;
        this.mp = mp;
        this.id = id;
    }
}
