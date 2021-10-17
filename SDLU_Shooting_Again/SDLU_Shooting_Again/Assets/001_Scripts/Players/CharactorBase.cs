using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
abstract public class CharactorBase : MonoBehaviour, IDamageable
{
    // 기본 변수
    public int ID { get; private set; }
    public bool IsRemote { get; private set; }
    public int MaxHP { get; private set; }

    // 피격 + 이동 용도
    protected Rigidbody rigid;
    protected BoxCollider coll;

    // 사용하는 변수
    protected int curHP;

    /// <summary>
    /// 새 플레이어 오브젝트 생성할 때 변수를 초기화함
    /// </summary>
    /// <param name="id">소켓의 id</param>
    /// <param name="maxHP">플레이어의 기본 HP</param>
    /// <param name="isRemote">로컬 플레이어의 캐릭터 여부</param>
    public virtual void Init(int id, int maxHP, bool isRemote = false)
    {
        this.ID = id;
        this.MaxHP = maxHP;
        this.IsRemote = isRemote;
    }

    public virtual void Damaged(int damage)
    {
        curHP -= damage;
    }
}
