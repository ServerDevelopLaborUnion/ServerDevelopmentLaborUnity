using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody))]
public class BoomW : SkillScript
{
    [SerializeField]
    private GameObject boomBarrier;
    [SerializeField]
    private float explosionSize;
    [SerializeField]
    private float boomDuration, fadeDuration, boomPower;
    [SerializeField] private float skillCoolTime = 0f;
    [SerializeField]
    private Material boomMaterial;
    private bool isBoom;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<ChickenPlayer>();
        //boomMaterial = boomBarrier.GetComponent<MeshRenderer>().material;
        Debug.Log(boomMaterial);
        //player.boomW += Boom;
        player.triggerEnter += HitBomb;
    }

    private void Update()
    {
        CoolDown(skillCoolTime);

        if (!canUse) return;

        UseSkill();
    }


    protected override void UseSkill()
    {
        base.UseSkill();
        if (Input.GetKeyDown(KeyCode.W) && !isBoom)
        {
            isBoom = true;
            Explosion();
        }
    }

    private void Explosion()
    {
        player.move -= player.getMove;
        rb.velocity = Vector3.zero;
        player.isMove = false;
        boomBarrier.SetActive(true);
        boomBarrier.transform.localScale = Vector3.one;
        boomMaterial.color = new Color(boomMaterial.color.r, boomMaterial.color.g, boomMaterial.color.b, 0.4f);
        boomBarrier.transform.DOScale(Vector3.one * explosionSize, boomDuration).OnComplete(() =>
        {
            boomMaterial.DOFade(0f, 1f).SetEase(Ease.InQuad).OnComplete(() =>
            {
                isBoom = false;
                boomBarrier.SetActive(false);
                player.move += player.getMove;
                canUse = false;
            });
        });

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
        player.move -= player.getMove;
        player.isMove = false;
        yield return new WaitUntil(() => player.IsGround() && rb.velocity.y < 0f);
        if (!isBoom)
        {
            player.move += player.getMove;
        }
    }

}