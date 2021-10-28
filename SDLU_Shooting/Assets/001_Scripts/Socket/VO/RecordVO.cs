using System;

[Serializable]
public class RecordVO
{
    public int kill;
    public int death;
    public int exp;

    public RecordVO(int kill, int death, int exp)
    {
        this.kill = kill;
        this.death = death;
        this.exp = exp;
    }
}
