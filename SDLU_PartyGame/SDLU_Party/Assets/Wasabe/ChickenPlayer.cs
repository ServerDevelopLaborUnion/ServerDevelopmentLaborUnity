using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class ChickenPlayer : Player
{
    #region Action
    public event Action<Collider> triggerEnter;
    #endregion

    private Rigidbody rb;
    private CapsuleCollider col;

    protected override void Awake()
    {
        base.Awake();
        triggerEnter += (a) => { };
    }

    private void Start()
    {
        col = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        Debug.Log(col.bounds.center);
    }
    protected override void Update()
    {
        base.Update();
        Debug.Log(IsGround());
    }


    private void OnTriggerEnter(Collider other)
    {
        if (IsGround())
        {
            triggerEnter(other);
        }
    }

    public bool IsGround()
    {
        return Physics.OverlapSphere(col.bounds.center, col.radius * col.height, LayerMask.GetMask("Ground")).Length > 0;
    }
}
