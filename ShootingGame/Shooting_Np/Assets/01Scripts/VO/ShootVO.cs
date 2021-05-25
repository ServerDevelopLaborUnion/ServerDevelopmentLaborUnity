using UnityEngine;
using System;

[Serializable]
public class ShootVO
{
    public Vector3 rotation;
    public int socketId;

    public ShootVO(int socketId, Vector3 rotation)
    {
        this.socketId = socketId;
        this.rotation = rotation;
    }

    public ShootVO()
    {

    }
}
