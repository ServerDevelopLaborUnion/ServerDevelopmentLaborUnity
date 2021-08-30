using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 키설정 용도

public class OptionInput : MonoBehaviour
{
    public KeyCode foward         = KeyCode.W;            // 앞으로 가는 키
    public KeyCode backward       = KeyCode.S;            // 뒤로 가는 키
    public KeyCode left           = KeyCode.A;            // 왼쪽으로 가는 키
    public KeyCode right          = KeyCode.D;            // 오른쪽으로 가는 키
    public KeyCode jump           = KeyCode.Space;        // 점프 키
    public KeyCode run            = KeyCode.LeftShift;    // 달리기 키
    public KeyCode crouch         = KeyCode.LeftControl;  // 누르는 동안 앉기 키
    public KeyCode toggleCrouch   = KeyCode.C;            // 앉기, 일어서기 전환 키
    public KeyCode reload         = KeyCode.R;            // 재장전 키
    public float mouseSensitivity = 1.0f;                 // 마우스 감도    
}
