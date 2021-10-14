using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 모든 케릭터가 상속받는 클레스

abstract public class CharactorBase : MonoBehaviour, IDamageable
{
    static public CharactorBase Instance { get; private set; }

    public bool IsRemote { get; set; }

    protected int hp = 100;
    public int ID { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    public virtual void OnDamage(int damage)
    {
        hp -= damage;
        
        if(hp <= 0)
        {
            Die();
        }

        SocketClient.Instance.Send(new DataVO("damage", damage.ToString()));
    }

    public virtual void OtherCharactorDamage(int id, string damage)
    {
        if(id == GameManager.instance.playerBase.ID)
        {
            hp -= int.Parse(damage);
        }
    }    

    protected virtual void Die()
    {
        this.gameObject.SetActive(false);
    }
}
