using System;
using UnityEngine;

public enum MouseInput
{
    Left = 0,
    Right,
    Wheel
}



[Serializable]
public class InputJson
{
    // 키보드 입력
    public KeyCode foward    = KeyCode.W;
    public KeyCode backward  = KeyCode.S;
    public KeyCode left      = KeyCode.A;
    public KeyCode right     = KeyCode.D;
    public KeyCode rollLeft  = KeyCode.Q;
    public KeyCode rollRight = KeyCode.E;
    public KeyCode up        = KeyCode.Space;
    public KeyCode down      = KeyCode.LeftShift;


    // 마우스 입력
    public MouseButton fire = MouseButton.Left;
    public MouseButton aim  = MouseButton.Right;

    // 감도
    public float mouseSensitivity = 1.0f;
}
