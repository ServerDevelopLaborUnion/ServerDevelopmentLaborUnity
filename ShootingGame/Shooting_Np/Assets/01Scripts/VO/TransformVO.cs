using UnityEngine;
using System;

[Serializable]
public class TransformVO
{
    public Vector2 point;
    public Vector3 rotation;
    public Vector3 turretRotation;
    public int socketId;

    public TransformVO(int socketId, Vector2 position, Vector3 rotation)
    {
        this.socketId = socketId;
        this.point = position;
        this.rotation = rotation;
    }

    public TransformVO(){

    }
}
