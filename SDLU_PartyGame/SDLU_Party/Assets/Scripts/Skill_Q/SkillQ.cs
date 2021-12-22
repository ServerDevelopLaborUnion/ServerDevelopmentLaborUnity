using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SkillQ : MonoBehaviour
{
    [SerializeField] private float skillRange = 0f;
    [SerializeField] private float skillSpeed = 0f;
    [SerializeField] private float skillPowerInHorizon = 0f;
    [SerializeField] private float skillPowerInVertical = 0f;
    [SerializeField] private GameObject rangeObject = null;
    [SerializeField] private Transform skillTransform = null;
    private float chargeTime;
    private bool isUsing = false;
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            Q(); 
            ShowRange(false);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            ShowRange(true);
        }
        else
        {
            chargeTime = Time.time;
        }


        if (isUsing)
        {
            PushBack(CheckColInRange());
        }
    }

    private void Q()
    {
        if (!isUsing)
        {
            isUsing = true;
            skillTransform.DOMove(new Vector3(transform.position.x, transform.position.y, skillRange), skillRange / skillSpeed).SetEase(Ease.Linear).onComplete += () => 
            {
                isUsing = false;
                skillTransform.localPosition = Vector3.zero;
            };
        }
    }

    private void ShowRange(bool isShow)
    {
        if (!isUsing || !isShow)
        {
            if (chargeTime - Time.time <= -0.2f || !isShow)
            {
                rangeObject.transform.DOScaleZ(isShow ? skillRange : 0, 0);
                rangeObject.SetActive(isShow);
            }
        }
    }

    Collider[] CheckColInRange()
    {
        Collider[] colliders = { null, };
        colliders = Physics.OverlapSphere(skillTransform.position, 1, LayerMask.GetMask("Players"));
        return colliders;
    }

    private void PushBack(Collider[] colliders)
    {
        foreach(var col in colliders)
        {
            col.GetComponent<Rigidbody>().AddForceAtPosition(Vector3.forward * skillPowerInVertical * 0.1f + Vector3.up * skillPowerInHorizon * 0.1f, skillTransform.position, ForceMode.Impulse);
        }
    }
}
