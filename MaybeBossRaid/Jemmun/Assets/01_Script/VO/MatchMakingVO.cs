using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MatchMakingVO
{
    public int players;
    public bool start;

    public MatchMakingVO(int players, bool start)
    {
        this.players = players;
        this.start = start;
    }
}
