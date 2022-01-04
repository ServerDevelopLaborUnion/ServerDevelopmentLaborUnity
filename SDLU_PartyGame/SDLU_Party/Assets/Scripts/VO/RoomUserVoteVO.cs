using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RoomUserVoteVO
{
    public int userID;

    public Dictionary<string, int> voteDictionary = new Dictionary<string, int>();

    public RoomUserVoteVO(int userID , Dictionary<string,int> voteDictionary) {
        this.userID = userID;
        this.voteDictionary = voteDictionary;
    }
}
