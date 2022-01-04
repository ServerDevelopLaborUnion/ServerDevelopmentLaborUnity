using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class VoteVO{
    public int gameID;

    public VoteVO(int gameID){
        this.gameID = gameID;
    }

}