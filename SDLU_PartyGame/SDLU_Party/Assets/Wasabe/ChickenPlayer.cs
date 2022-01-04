using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class ChickenPlayer : Player
{
    public int ID { get; set; }

    #region Action
    public event Action<Collider> triggerEnter;
    #endregion

    private CapsuleCollider col;
    [SerializeField] private ParticleSystem deadParticle = null;
    private AudioSource adSo;

    protected override void Awake()
    {
        base.Awake();
        triggerEnter += (a) => { };

    }

    private void Start()
    {
        col = GetComponent<CapsuleCollider>();
        adSo = GetComponent<AudioSource>();
    }

    protected override void Update()
    {
        base.Update();
        if (!CheckPlayerInArea())
        {
            StartCoroutine(Eliminated());
            if (isMe)
                EnvironmentManager.instance.DeadEnvironment();
            isPlaying = false;
        }
        if(CheckPlayerInArea())
        {
            gameObject.SetActive(true);
            if(isMe)
                EnvironmentManager.instance.AliveEnvironment();
            isPlaying = true;
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
        if(Vector3.Distance(Vector3.zero + Vector3.up * transform.position.y, transform.position) >= 16)
            return false;
        return true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("왜 안될까");
        if (IsGround())
        {
            triggerEnter(other);
        }
    }

    public bool IsGround()
    {
        return Physics.OverlapSphere(col.bounds.center, col.radius * col.height, LayerMask.GetMask("Ground")).Length >= 0;
    }

    public void PlaySound(float pitch)
    {
        adSo.pitch = pitch;
        adSo.Play();
    }

}
