using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 모든 케릭터가 상속받는 클레스

abstract public class CharactorBase : MonoBehaviour
{
    public bool IsRemote { get; set; }
    
    protected Rigidbody rigid = null; // 이동 위한


    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

}
