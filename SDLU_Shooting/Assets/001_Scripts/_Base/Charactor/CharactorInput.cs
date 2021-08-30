using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 모든 캐릭터가 상속받는 이동 클레스

public class CharactorInput : MonoBehaviour
{
    [Header("최대 속도")]
    [SerializeField] protected float maxSpeed = 10.0f;




    // TODO : 임시 스크립트
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            BasicControl.Play(MouseButton.Left);
        }
    }
}
