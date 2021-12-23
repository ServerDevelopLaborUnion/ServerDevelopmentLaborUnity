using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SkillQ : SkillScript
{
    [SerializeField] private float skillRange = 0f;
    [SerializeField] private float skillSpeed = 0f;
    [SerializeField] private float skillPowerInHorizon = 0f;
    [SerializeField] private float skillPowerInVertical = 0f;
    [SerializeField] private float skillCoolTime = 0f;
    [SerializeField] private GameObject rangeObject = null;
    [SerializeField] private Transform skillTransform = null;
    private float chargeTime;
    private bool isUsing = false;
    void Update()
    {
        CoolDown(skillCoolTime, ref canUse);

        if (!canUse) return;

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
            skillTransform.gameObject.SetActive(true);
            skillTransform.SetParent(null);
            isUsing = true;
            skillTransform.DOMove(skillTransform.forward * skillRange, skillRange / skillSpeed).SetEase(Ease.Linear).onComplete += () =>
            {
                canUse = false;
                isUsing = false;
                skillTransform.SetParent(this.transform);
                skillTransform.gameObject.SetActive(false);
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
            col.GetComponent<Rigidbody>().AddForceAtPosition(skillTransform.forward * skillPowerInVertical * 0.1f + Vector3.up * skillPowerInVertical * 0.1f, skillTransform.position, ForceMode.Impulse);
        }
    }
}
