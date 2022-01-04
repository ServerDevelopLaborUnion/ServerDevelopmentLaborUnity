using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RoomStartVoteVO
{
    public int userCount;


    public RoomStartVoteVO(int userCount) {
        this.userCount = userCount;
    }
}
