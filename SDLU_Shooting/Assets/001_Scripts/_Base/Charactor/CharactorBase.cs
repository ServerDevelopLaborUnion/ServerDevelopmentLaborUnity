using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 모든 케릭터가 상속받는 클레스

abstract public class CharactorBase : MonoBehaviour, IDamageable
{
    public bool IsRemote { get; set; }

    protected int hp = 100;
    public int ID { get; set; }

    public virtual void OnDamage(int damage)
    {
        hp -= damage;
        
        if(hp <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        this.gameObject.SetActive(false);
    }
}
