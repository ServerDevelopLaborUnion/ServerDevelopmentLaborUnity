using UnityEngine;
using System;

[Serializable]
public class OnDamageVO
{
    public int socketId;
    public float damage;

    public OnDamageVO(int socketId, float damage)
    {
        this.socketId = socketId;
        this.damage = damage;
    }

    public OnDamageVO()
    {

    }
}
