using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class ChickenPlayer : Player
{
    [SerializeField]
    private GameObject boomBarrier;
    [SerializeField]
    private float explosionSize;
    [SerializeField]
    private float boomDuration, fadeDuration, boomPower;

    #region Action
    public event Action<Collider> triggerEnter;
    public event Action boom;
    #endregion

    private Material boomMaterial;
    private bool isBoom;

    private Rigidbody rb;
    private CapsuleCollider col;

    protected override void Awake()
    {
        base.Awake();
        triggerEnter += (a) => { };
        boom += () => { };
    }

    private void Start()
    {
        col = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        boomMaterial = boomBarrier.GetComponent<MeshRenderer>().material;
        Debug.Log(col.bounds.center);
    }
    protected override void Update()
    {
        base.Update();
        boom();
        Debug.Log(IsGround());
    }

    protected override void Initvalue()
    {
        base.Initvalue();
        triggerEnter += HitBomb;
        boom += Boom;
    }
    private void Boom()
    {
        if (Input.GetKeyDown(KeyCode.W) && !isBoom)
        {
            isBoom = true;
            Explosion();
        }
    }

    private void Explosion()
    {
        move -= getMove;
        isMove = false;
        boomBarrier.SetActive(true);
        boomBarrier.transform.localScale = Vector3.one;
        boomMaterial.color = new Color(boomMaterial.color.r, boomMaterial.color.g, boomMaterial.color.b, 0.6f);
        boomBarrier.transform.DOScale(Vector3.one * explosionSize, boomDuration).OnComplete(() =>
        {
            boomMaterial.DOFade(0f, 1f).SetEase(Ease.InQuad).OnComplete(() =>
            {
                isBoom = false;
                boomBarrier.SetActive(false);
                move += getMove;
            });
        });

    }

    private void OnTriggerEnter(Collider other)
    {
        triggerEnter(other);
    }

    private void HitBomb(Collider other)
    {
        if (other.CompareTag("Boom"))
        {
            if (other.gameObject == boomBarrier) return;

            Vector3 distance;

            distance = other.transform.position - transform.position;

            distance.Normalize();
            rb.AddForce(new Vector3(-distance.x, distance.y + 1, -distance.z) * boomPower, ForceMode.Impulse);
            StartCoroutine(WaitIsground());
        }
    }

    private IEnumerator WaitIsground()
    {
        move -= getMove;
        isMove = false;
        yield return new WaitUntil(() => IsGround() && rb.velocity.y < 0f);
        move += getMove;
    }

    private bool IsGround()
    {
        return Physics.OverlapSphere(col.bounds.center, col.radius * col.height, LayerMask.GetMask("Ground")).Length > 0;
    }
}