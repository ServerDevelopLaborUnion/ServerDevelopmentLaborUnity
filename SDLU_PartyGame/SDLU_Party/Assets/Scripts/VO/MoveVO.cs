using System;
using UnityEngine;

[Serializable]
public class MoveVO
{
    public int id;
    public Vector3 position;
    public Quaternion rotation;

    public MoveVO(int id, Vector3 position, Quaternion rotation)
    {
        this.id = id;
        this.position = position;
        this.rotation = rotation;
    }
}