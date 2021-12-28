using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 offset;

    void Start()
    {
        LookTarget(target);  
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Space)){
            LookTarget(target);
        }
        //마우스가 화면 끝으로 이동하면 카메라 위치를 변경한다.
        if(Input.mousePosition.x >= Screen.width - 10f){
            transform.localPosition += new Vector3(0f, 0f, 0.1f);
        }
        if(Input.mousePosition.x <= 10f){
            transform.localPosition += new Vector3(0, 0f, -0.1f);
        }
        if(Input.mousePosition.y >= Screen.height - 10f){
            transform.localPosition += new Vector3(-0.1f, 0f, 0f);
        }
        if(Input.mousePosition.y <= 10f){
            transform.localPosition += new Vector3(0.1f, 0f, 0f);
        }
    }

    private void LookTarget(Transform target){
        //move lerp
        transform.position = Vector3.Lerp(transform.position, target.position + offset, 1f);
        transform.LookAt(target);
    }

}
