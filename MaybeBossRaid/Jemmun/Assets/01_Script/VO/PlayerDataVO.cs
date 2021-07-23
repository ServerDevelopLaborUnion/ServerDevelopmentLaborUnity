using System;
using System.Collections.Generic;

[Serializable]
public class PlayerDataVO
{
    public int hp;
    public int mp;
    public int id;
    public JobList job;

    public PlayerDataVO(int id, int hp, int mp, JobList job)
    {
        this.hp = hp;
        this.mp = mp;
        this.id = id;
        this.job = job;
    }
}