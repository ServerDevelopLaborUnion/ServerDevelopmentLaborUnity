using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody))]
public class BoomW : MonoBehaviour
{
    [SerializeField]
    private GameObject boom;
    [SerializeField]
    private float explosionSize;
    [SerializeField]
    private float boomDuration, fadeDuration,boomPower;


    private Material boomMaterial;
    private bool isBoom;  // 대충 Action에서 move빼는것 대용

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        boomMaterial = boom.GetComponent<MeshRenderer>().material;
        Debug.Log(boomMaterial);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && !isBoom)
        {
            isBoom = true;
            Explosion();
        }
    }


    private void Explosion()
    {
        boom.SetActive(true);
        boom.transform.localScale = Vector3.one;
        boomMaterial.color = new Color(boomMaterial.color.r, boomMaterial.color.g, boomMaterial.color.b, 0.6f);
        boom.transform.DOScale(Vector3.one * explosionSize, boomDuration).OnComplete(() =>
        {
            boomMaterial.DOFade(0f, 1f).SetEase(Ease.InQuad).OnComplete(() =>
            {
                isBoom = false;
                boom.SetActive(false);
            });
        });

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boom"))
        {
            if (other.gameObject == boom) return;

            Vector3 distance;

            distance = other.transform.position - transform.position;

            distance.Normalize();
            Debug.Log(other);
            Debug.Log(distance);
            Debug.Log(distance * boomPower);
            rb.AddForce(new Vector3(-distance.x, distance.y + 1, -distance.z) * boomPower, ForceMode.Impulse);
            Debug.Log(rb.velocity);
        }
    }

}
