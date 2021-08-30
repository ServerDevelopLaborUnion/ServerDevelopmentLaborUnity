using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 키설정 용도 클래스

public enum MouseButton // 마우스 버튼
{
    Left = 0,
    Middle = 1,
    Right = 2
}

[Serializable] // 저장을 위한
public class OptionInput : MonoBehaviour
{
    // 캐릭터 이동
    public KeyCode foward         = KeyCode.W;            // 앞으로 가는 키
    public KeyCode backward       = KeyCode.S;            // 뒤로 가는 키
    public KeyCode left           = KeyCode.A;            // 왼쪽으로 가는 키
    public KeyCode right          = KeyCode.D;            // 오른쪽으로 가는 키
    public KeyCode jump           = KeyCode.Space;        // 점프 키
    public KeyCode run            = KeyCode.LeftShift;    // 달리기 키
    public KeyCode walk           = KeyCode.LeftAlt;      // 걷기 키
    public KeyCode crouch         = KeyCode.LeftControl;  // 누르는 동안 앉기 키
    public KeyCode toggleCrouch   = KeyCode.C;            // 앉기, 일어서기 전환 키
    // 캐릭터 이동

    // 전투
    public KeyCode reload         = KeyCode.R;            // 재장전 키
    public MouseButton aim        = MouseButton.Right;    // 조준
    public MouseButton fire       = MouseButton.Left;     // 발사
    public KeyCode throwable      = KeyCode.G;            // 투척 무기
    // 전투

    // 시점
    public float mouseSensitivity = 1.0f;                 // 마우스 감도 
    // 시점
}
