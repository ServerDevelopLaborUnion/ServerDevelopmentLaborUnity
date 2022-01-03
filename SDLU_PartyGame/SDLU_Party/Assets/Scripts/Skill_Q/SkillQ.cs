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
    private bool isHolding = false;

    protected override void Start()
    {
        base.Start();
        key = 0;
        rb = GetComponent<Rigidbody>();
        boxSizeZ = skillBlock.transform.localScale.z;
        int count = Mathf.RoundToInt(skillRange / boxSizeZ + 0.1f);
        player = GetComponent<ChickenPlayer>();
        player.triggerEnter += HitQ;
        for (int i = 1; i <= count; i++)
        {
            GameObject g = Instantiate(skillBlock, skillObjectTransform);
            g.transform.position += new Vector3(0f, 0f, (boxSizeZ + 0.1f) * i);
            g.SetActive(false);
            blockList.Add(g);
        }
    }

    protected override void Update()
    {
        base.Update();
        if (!hasStarted) return;
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
            skillTransform.DOMove(skillObjectTransform.forward * skillRange + skillObjectTransform.position, skillRange / skillSpeed).SetEase(Ease.Linear).onComplete += () =>
            {
                canUse = false;
                isUsing = false;
                skillTransform.SetParent(skillObjectTransform);
                skillTransform.gameObject.SetActive(false);
                skillTransform.localPosition = Vector3.zero;
                skillTransform.localRotation = Quaternion.Euler(Vector3.zero + Vector3.forward * 90);
            };
            StartCoroutine(UpdownBlock());

        }
    }
    private IEnumerator UpdownBlock()
    {

        for (int i = 0; i < blockList.Count; i++)
        {
            blockList[i].SetActive(true);
            blockList[i].transform.SetParent(null);
        }
        for (int i = 0; i < blockList.Count; i++)
        {
            blockTransform = blockList[i].transform;
            float duration = 1 / skillSpeed;
            Debug.Log(duration);
            Debug.Log(blockList[i].transform.position.y);
            blockTransform.DOMoveY(transform.position.y, duration).OnComplete(() =>
            {
                blockTransform.DOMoveY(/*blockTransform.position.y*/-2f, duration * 10);
            });
            yield return Yields.WaitSeconds(duration);
        }

        for (int i = 0; i < blockList.Count; i++)
        {
            blockList[i].SetActive(false);
            blockList[i].transform.SetParent(skillObjectTransform);
            blockList[i].transform.localPosition = new Vector3(0, -2f, 0);
            blockList[i].transform.localRotation = Quaternion.Euler(Vector3.zero);
            blockList[i].transform.localPosition += new Vector3(0f, 0f, (boxSizeZ + 0.1f) * i);
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
    private void HitQ(Collider other)
    {
        if (other.CompareTag("SkillQ"))
        {
            Debug.Log("힘 들어감");
            if (other.gameObject == skillTransform.gameObject) return;
            Vector3 distance;

            distance = other.transform.position - skillTransform.position;

            distance.Normalize();
            rb.AddForce(new Vector3(-distance.x * skillPowerInHorizon, distance.y * skillPowerInVertical, -distance.z), ForceMode.Impulse);
            //rb.AddForceAtPosition(skillTransform.forward * skillPowerInVertical * 0.1f + Vector3.up * skillPowerInVertical * 0.1f, skillTransform.position, ForceMode.Impulse);
            canUse = false;
            StartCoroutine(WaitIsground());
        }
    }
    // Collider[] CheckColInRange()
    // {
    //     Collider[] colliders = { null, };
    //     colliders = Physics.OverlapSphere(skillTransform.position, 3, LayerMask.GetMask("Enemies"));
    //     return colliders;
    // }

    // private void PushBack(Collider[] colliders)
    // {
    //     foreach (var col in colliders)
    //     {
    //         col.GetComponent<Rigidbody>().AddForceAtPosition(skillTransform.forward * skillPowerInVertical * 0.1f + Vector3.up * skillPowerInVertical * 0.1f, skillTransform.position, ForceMode.Impulse);
    //     }
    // }
    private IEnumerator WaitIsground()
    {
        player.isMove = false;
        yield return new WaitUntil(() => player.IsGround() && rb.velocity.y < 0f);
        canUse = true;
    }
    private void UseSkill()
    {
        if (!base.CheckSkillAvailable()) return;
        if (Input.GetKeyUp(KeyCode.Q))
        {
            Q();
            isHolding = false;
            ShowRange(false);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            isHolding = true;
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

        // if (isUsing&& !isHolding)
        // {
        //     PushBack(CheckColInRange());
        // }
    }
}
