using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLockManager : MonoSingleton<MouseLockManager>
{
    public void LockMouse()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UnLockMouse()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
