using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public partial class CharactorBase : MonoBehaviour, IDamageable
{
    public int hp  = 100;
    public int mp  = 100;
    public int pos = 0; // �� �ϴ� ���������� �̹� ���Ƹ� �ð��� ���� �� �帮�ڽ�
    public int id  = 0;
    //protected string name; // �ϴ� �ּ�ó�� �� �ΰٽ����.

    public bool isRemote = false; // ���� ĳ��������

    /// <summary>
    /// ������ ó�� �ϴ� �Լ�
    /// </summary>
    /// <param name="damage">������, ���� ��� ������ ���Ϳ�</param>
    abstract public void OnDamage(int damage);
}
 
abstract public partial class CharactorBase : MonoBehaviour, IDamageable
{
    
}