using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SkillQ : SkillScript
{
    private List<GameObject> blockList = new List<GameObject>();

    [SerializeField] private float skillRange = 0f;
    [SerializeField] private float skillSpeed = 0f;
    [SerializeField] private float skillPowerInHorizon = 0f;
    [SerializeField] private float skillPowerInVertical = 0f;
    [SerializeField] private Transform skillObjectTransform = null;
    [SerializeField] private Transform skillTransform = null;
    [SerializeField] private GameObject skillBlock = null;
    [SerializeField] private GameObject rangeObject = null;
    [SerializeField] private float skillCoolTime = 0f;
    
    private Transform blockTransform;

    private float chargeTime;
    private float boxSizeZ;

    private bool isUsing = false;

    private void Start()
    {
        boxSizeZ = skillBlock.transform.localScale.z;
        int count = Mathf.RoundToInt(skillRange / boxSizeZ + 0.1f);
        player = GetComponent<ChickenPlayer>();
        for (int i = 1; i <= count; i++){
            GameObject g = Instantiate(skillBlock , skillTransform);
            g.transform.position += new Vector3(0f, 0f, (boxSizeZ + 0.1f) * i);
            blockList.Add(g);
        }
    }

    private void Update()
    {
        CoolDown(skillCoolTime);

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
            StartCoroutine(UpdownBlock());
            // for (int i = 0; i < count; i++)
            // {
            //     GameObject block = Instantiate(skillBlock);
            //     blockTransform = block.transform;
            //     blockTransform.position += new Vector3(0f, 0f, boxSizeZ + 0.1f);
            //     blockTransform.DOMoveY(transform.parent.parent.position.y ,boxSizeZ )

            // }

        }
    }
    private IEnumerator UpdownBlock(){
        for (int i = 0; i < blockList.Count; i++){
            blockTransform = blockList[i].transform;
            float duration = 1 / skillSpeed;
            //float duration = Vector3.Distance(blockTransform.position, transform.position)/skillSpeed;
            Debug.Log(duration);
            Debug.Log(blockList[i].transform.position.y);
            blockTransform.DOMoveY(transform.position.y, duration).OnComplete(() =>
            {
                blockTransform.DOMoveY(/*blockTransform.position.y*/-2f, duration*2);
            });
            yield return Yields.WaitSeconds(duration);
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
        foreach (var col in colliders)
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
            player.Rotate(skillObjectTransform, hit.point);
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
