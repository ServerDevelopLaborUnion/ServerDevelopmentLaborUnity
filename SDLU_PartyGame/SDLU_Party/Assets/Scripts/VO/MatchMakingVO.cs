using System;

[Serializable]
public class MatchMakingVO
{
    public string type;

    public MatchMakingVO(string type)
    {
        this.type = type;
    }
}