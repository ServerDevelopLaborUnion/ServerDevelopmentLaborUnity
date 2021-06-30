using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class CharactorBase : MonoBehaviour, IDamageable
{
    protected int hp = 100;
    protected int mp = 100;
    protected int pos = 0; // 뭐 하는 변수인지는 이번 동아리 시간에 설명 해 드리겠슴
    //protected string name; // 일단 주석처리 해 두겟스빈다.


    public virtual void OnDamage(int damage)
    {
        hp -= damage;
    }
}
