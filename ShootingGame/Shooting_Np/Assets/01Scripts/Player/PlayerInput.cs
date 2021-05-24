using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public string frontAxisName = "Vertical";
    public string rightAxisName = "Horizontal";
    public string fireButtonName = "Fire1";

    public float frontMove { get; private set; } //감지된 앞뒤 움직임 입력값
    public float rightMove { get; private set; } //감지된 좌우 움직임 입력값
    public bool fire { get; private set; } //발사 입력값

    public Vector3 mousePos { get; private set; } // 마우스 포인터 위치

    void Update()
    {
        frontMove = Input.GetAxis(frontAxisName);
        rightMove = Input.GetAxis(rightAxisName);
        fire = Input.GetButtonDown(fireButtonName);

        Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mp.z = 0;
        mousePos = mp;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(mousePos, 0.1f);
    }
}
