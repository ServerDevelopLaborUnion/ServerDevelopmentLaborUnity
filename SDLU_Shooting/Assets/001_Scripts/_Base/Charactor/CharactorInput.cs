using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 모든 캐릭터가 사용하는 이동 클레스
// 키 입력을 받았는지 판단하는 클레스입니다.

public class CharactorInput : MonoBehaviour
{
    public bool Shoot     { get; private set; }
    public bool Aim       { get; private set; }
    public bool Foward    { get; private set; }
    public bool Backward  { get; private set; }
    public bool Left      { get; private set; }
    public bool Right     { get; private set; }
    public bool Up        { get; private set; }
    public bool Down      { get; private set; }
    public bool RollLeft  { get; private set; }
    public bool RollRight { get; private set; }
    public bool Run       { get; private set; }
    public bool Reload    { get; private set; }

    void Update()
    {
        Shoot = Input.GetMouseButton((int)MouseButton.Left);
        Aim   = Input.GetMouseButton((int)MouseButton.Right);

        Foward    = Input.GetKey(OptionInput.instance.foward);
        Backward  = Input.GetKey(OptionInput.instance.backward);
        Left      = Input.GetKey(OptionInput.instance.left);
        Right     = Input.GetKey(OptionInput.instance.right);
        Up        = Input.GetKey(OptionInput.instance.up);
        Down      = Input.GetKey(OptionInput.instance.down);
        RollLeft  = Input.GetKey(OptionInput.instance.rollLeft);
        RollRight = Input.GetKey(OptionInput.instance.rollRight);
        Run       = Input.GetKey(OptionInput.instance.run);

        Reload    = Input.GetKey(OptionInput.instance.reload);
    }
}
