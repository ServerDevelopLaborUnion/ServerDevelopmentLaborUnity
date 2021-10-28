using System;
using UnityEngine;

[Serializable]
public class MoveVO 
{
    public Vector3 pos;
    public Vector3 rot; 
    public int id;
    public MoveVO( Vector3 pos, Vector3 rot, int id)
    {
        this.pos = pos;
        this.rot = rot;
        this.id = id;
    }
}
