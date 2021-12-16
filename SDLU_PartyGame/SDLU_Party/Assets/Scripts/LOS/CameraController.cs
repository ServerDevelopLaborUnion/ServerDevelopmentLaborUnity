using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 offset;

    void Update()
    {
        //camera follows the player
        transform.position = target.position + offset;
        transform.LookAt(target);
    }
}
