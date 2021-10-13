using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 마우스에 관한 것을 다루는 클레스

public class MouseManager : MonoBehaviour
{
    static public MouseManager inst = null;

    private void Awake()
    {
        inst = this;
        MouseLocked = true; // TODO : 테스트 용 코드
    } 

    void Update()
    {
        switch(MouseLocked)
        {
            case true:  LockMouse();   break;
            case false: UnlockMosue(); break;
        }


    }

    private void LockMouse() // 마우스를 게임 화면에 잡아둠
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void UnlockMosue()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    static public bool MouseLocked { get; set; }
}
