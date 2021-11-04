using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemotePlayer : CharactorBase
{
    [SerializeField] private float lerpAmount = 0.7f;

    Vector3 targetPos = new Vector3();
    Vector3 tartetRot = new Vector3();

    private void Awake() {
        targetPos = transform.position;
        tartetRot = transform.eulerAngles;
    }


    public void SetTargetTransform(Vector3 pos, Vector3 rot)
    {
        targetPos = pos;
        tartetRot = rot;
    }


    private void Update()
    {
        transform.position    = Vector3.Lerp(transform.position,    targetPos, lerpAmount);
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, targetPos, lerpAmount);
    }
}
