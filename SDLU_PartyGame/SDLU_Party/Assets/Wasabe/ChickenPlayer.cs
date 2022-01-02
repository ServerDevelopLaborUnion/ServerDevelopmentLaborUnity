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
    [SerializeField] private ParticleSystem deadParticle = null;

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
        if (!CheckPlayerInArea())
        {
            StartCoroutine(Eliminated());
            if(isPlaying)
                EnvironmentManager.instance.DeadEnvironment();
        }
        if(CheckPlayerInArea())
        {
            gameObject.SetActive(true);
            if(isPlaying)
                EnvironmentManager.instance.AliveEnvironment();
        }
    }

    private IEnumerator Eliminated()
    {
        if (isDead) yield break;
        isDead = true;
        deadParticle.Play();
        meshRenderer.enabled = false;
        yield return Yields.WaitSeconds(1f);
        gameObject.SetActive(false);
    }

    private bool CheckPlayerInArea()
    {
        if(Vector3.Distance(Vector3.zero + Vector3.up * transform.position.y, transform.position) >= 20)
            return false;
        return true;
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
