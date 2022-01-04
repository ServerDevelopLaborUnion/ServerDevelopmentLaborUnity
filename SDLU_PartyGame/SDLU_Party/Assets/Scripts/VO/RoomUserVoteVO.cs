using System;
using System.Collections.Generic;

[Serializable]
public class RoomUserVoteVO
{

    public Dictionary<string, int> voteDictionary = new Dictionary<string, int>();

    public RoomUserVoteVO(Dictionary<string,int> voteDictionary) {
        this.voteDictionary = voteDictionary;
    }
}
