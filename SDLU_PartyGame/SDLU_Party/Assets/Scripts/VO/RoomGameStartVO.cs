using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RoomGameStartVO
{
    public int gameNumber;

    public RoomGameStartVO(int gameNumber)
    {
        this.gameNumber = gameNumber;
    }
}
