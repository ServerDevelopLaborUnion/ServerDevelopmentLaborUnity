using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoSingleton<InputManager>
{
    #region 이동
    /// <summary>
    /// 앞으로 가는 키 눌렀을 때 계속 호출됨
    /// </summary>
    public event Action OnKeyFoward;
    /// <summary>
    /// 뒤로 가는 키 눌렀을 때 계속 호출됨
    /// </summary>
    public event Action OnKeyBackWard;
    /// <summary>
    /// 왼쪽으로 가는 키 눌렀을 때 계속 호출됨
    /// </summary>
    public event Action OnKeyLeft;
    /// <summary>
    /// 오른쪽으로 가는 키 눌렀을 때 계속 호출됨
    /// </summary>
    public event Action OnKeyRight;
    /// <summary>
    /// 위로 가는 키 눌렀을 때 계속 호출됨
    /// </summary>
    public event Action OnKeyUp;
    /// <summary>
    /// 아레로 가는 키 눌렀을 때 계속 호출됨
    /// </summary>
    public event Action OnKeyDown;
    /// <summary>
    /// 왼쪽으로 도는 가는 키 눌렀을 때 계속 호출됨
    /// </summary>
    public event Action OnKeyRollLeft;
    /// <summary>
    /// 오른쪽으로 도는 가는 키 눌렀을 때 계속 호출됨
    /// </summary>
    public event Action OnKeyRollRight;
    #endregion

    #region 마우스
    /// <summary>
    /// 발사 마우스 입력을 받았을 때 계속 실행됨
    /// </summary>
    public event Action OnMouseFire;

    /// <summary>
    /// 조준 마우스 버튼을 받았을 때 계속 실행됨
    /// </summary>
    public event Action OnMouseAim;
    #endregion

    #region 기타

    public event Action OnKeyReload;

    #endregion



    private InputJson keys = new InputJson();


    private void Awake()
    {
        #region 이동
        OnKeyFoward    += () => { };
        OnKeyBackWard  += () => { };
        OnKeyLeft      += () => { };
        OnKeyRight     += () => { };
        OnKeyUp        += () => { };
        OnKeyDown      += () => { };
        OnKeyRollLeft  += () => { };
        OnKeyRollRight += () => { };
        #endregion

        #region 마우스
        OnMouseFire    += () => { };
        OnMouseAim     += () => { };
        #endregion

        #region 기타
        OnKeyReload    += () => { };
        #endregion
    }



    private void Update()
    {
        #region 이동
        if (Input.GetKey(keys.foward))
        {
            OnKeyFoward();
        }
        if (Input.GetKey(keys.backward))
        {
            OnKeyBackWard();
        }
        if (Input.GetKey(keys.left))
        {
            OnKeyLeft();
        }
        if (Input.GetKey(keys.right))
        {
            OnKeyRight();
        }
        if (Input.GetKey(keys.rollLeft))
        {
            OnKeyRollLeft();
        }
        if (Input.GetKey(keys.rollRight))
        {
            OnKeyRollRight();
        }
        if (Input.GetKey(keys.up))
        {
            OnKeyUp();
        }
        if (Input.GetKey(keys.down))
        {
            OnKeyDown();
        }
        #endregion

        #region 마우스

        if(Input.GetMouseButton((int)keys.fire))
        {
            OnMouseFire();
        }
        if(Input.GetMouseButton((int)keys.aim))
        {
            OnMouseAim();
        }

        #endregion
    
        #region 기타

        if(Input.GetKeyDown(keys.reload))
        {
            OnKeyReload();
        }

        #endregion

    }

    public float GetMouseSensitivity()
    {
        return keys.mouseSensitivity;
    }

    public void SetMouseSensitivity(float value)
    {
        keys.mouseSensitivity = value;
    }

}
