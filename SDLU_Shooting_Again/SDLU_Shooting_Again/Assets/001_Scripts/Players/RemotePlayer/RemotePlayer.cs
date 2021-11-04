using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemotePlayer : CharactorBase
{
    [SerializeField] private float lerpAmount = 0.7f;

    Vector3 targetPos = new Vector3();
    Vector3 targetRot = new Vector3();

    private void Awake() {
        targetPos = transform.position;
        targetRot = transform.eulerAngles;
    }


    public void SetTargetTransform(Vector3 pos, Vector3 rot)
    {
        targetPos = pos;
        targetRot = rot;
    }


    private void Update()
    {
        if(Vector3.Distance(targetPos, transform.position) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, lerpAmount);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetRot), lerpAmount);

    }
}
