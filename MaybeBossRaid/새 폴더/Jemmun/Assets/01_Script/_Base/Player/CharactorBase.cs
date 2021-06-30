using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class CharactorBase : MonoBehaviour, IDamageable
{
    protected int hp = 100;
    protected int mp = 100;
    protected int pos = 0; // �� �ϴ� ���������� �̹� ���Ƹ� �ð��� ���� �� �帮�ڽ�
    //protected string name; // �ϴ� �ּ�ó�� �� �ΰٽ����.


    public virtual void OnDamage(int damage)
    {
        hp -= damage;
    }
}
