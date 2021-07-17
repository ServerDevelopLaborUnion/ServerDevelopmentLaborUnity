using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public partial class CharactorBase : MonoBehaviour, IDamageable
{
    public int hp  = 100;
    public int mp  = 100;
    public int pos = 0; // 뭐 하는 변수인지는 이번 동아리 시간에 설명 해 드리겠슴
    public int id  = 0;
    //protected string name; // 일단 주석처리 해 두겟스빈다.

    public bool isRemote = false; // 본인 캐릭터인지

    /// <summary>
    /// 데미지 처리 하는 함수
    /// </summary>
    /// <param name="damage">데미지, 힐인 경우 음수로 들어와요</param>
    abstract public void OnDamage(int damage);
}
 
abstract public partial class CharactorBase : MonoBehaviour, IDamageable
{
    
}