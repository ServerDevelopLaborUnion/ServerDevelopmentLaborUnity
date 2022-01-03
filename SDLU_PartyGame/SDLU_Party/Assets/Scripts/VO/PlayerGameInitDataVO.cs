using System;
using UnityEngine;

[Serializable]
public class PlayerGameInitDataVO
{
    public int id;
    public Vector2 position;

    public PlayerGameInitDataVO(int id, Vector2 position)
    {
        this.id = id;
        this.position = position;
    }
}