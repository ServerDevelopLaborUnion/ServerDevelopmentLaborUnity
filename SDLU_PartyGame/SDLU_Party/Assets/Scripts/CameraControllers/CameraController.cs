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
    }

    private void LookTarget(Transform target){
        //move lerp
        transform.position = Vector3.Lerp(transform.position, target.position + offset, 1f);
        transform.LookAt(target);
    }

}
