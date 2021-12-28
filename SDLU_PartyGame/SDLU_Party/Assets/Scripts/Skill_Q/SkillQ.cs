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
    [SerializeField] private Transform skillObjectTransform = null;
    [SerializeField] private GameObject rangeObject = null;
    [SerializeField] private Transform skillTransform = null;
    private float chargeTime;
    private bool isUsing = false;

    protected override void Update()
    {
        CoolDown(skillCoolTime, ref canUse);

        if (!canUse) return;

        UseSkill();
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
                skillTransform.SetParent(skillObjectTransform);
                skillTransform.gameObject.SetActive(false);
                skillTransform.localPosition = Vector3.zero;
                skillTransform.localRotation = Quaternion.Euler(Vector3.zero + Vector3.forward * 90);
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
        colliders = Physics.OverlapSphere(skillTransform.position, 3, LayerMask.GetMask("Players"));
        return colliders;
    }

    private void PushBack(Collider[] colliders)
    {
        foreach(var col in colliders)
        {
            col.GetComponent<Rigidbody>().AddForceAtPosition(skillTransform.forward * skillPowerInVertical * 0.1f + Vector3.up * skillPowerInVertical * 0.1f, skillTransform.position, ForceMode.Impulse);
        }
    }

    protected override void UseSkill()
    {
        base.UseSkill();
        if (Input.GetKeyUp(KeyCode.Q))
        {
            Q();
            ShowRange(false);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground"));
            Rotate(skillObjectTransform,hit.point);
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
}
