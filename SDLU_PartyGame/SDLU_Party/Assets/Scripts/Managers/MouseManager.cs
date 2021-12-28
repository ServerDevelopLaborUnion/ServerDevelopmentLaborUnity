using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }
}
