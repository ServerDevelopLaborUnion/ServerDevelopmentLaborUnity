using System;
using UnityEngine;

[Serializable]
public class MoveVO 
{
    public string pos;
    public string rot; 
    public int id;
    public MoveVO( Vector3 pos, Vector3 rot, int id)
    {
        this.pos = JsonUtility.ToJson(pos);
        this.rot = JsonUtility.ToJson(rot);
        this.id = id;
    }
}
