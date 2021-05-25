using UnityEngine;
using System;

[Serializable]
public class DisconnectedVO
{
    public int socketId;

    public DisconnectedVO(int _socketId)
    {
        socketId = _socketId;
    }

    public DisconnectedVO()
    {

    }
}
