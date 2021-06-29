using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class CharactorBase : MonoBehaviour, IDamageable
{
    protected int hp = 100;
    protected int mp = 100;
    //protected string name; // 老窜 林籍贸府 秦 滴百胶后促.



    public virtual void OnDamage(int damage)
    {
        hp -= damage;
    }
}
