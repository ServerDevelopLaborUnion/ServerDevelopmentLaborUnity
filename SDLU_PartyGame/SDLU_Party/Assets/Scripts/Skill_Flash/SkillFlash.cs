using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFlash : SkillScript
{
    [SerializeField]
    private float maxFlashDistance = 7f;
    [SerializeField] private float skillCoolTime = 0f;
    [SerializeField] private ParticleSystem flashParticle = null;

    protected override void Start()
    {
        base.Start();
        key = 2;
    }

    protected override void Update()
    {
        base.Update();
        if (!hasStarted) return;

        CoolDown(skillCoolTime);

        if (!canUse) return;
        UseSkill();
    }

    private void Flash()
    {
        StartCoroutine(CreateEffect());
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground"));
        Vector3 dest = hit.point;
        if (Vector3.Distance(dest, transform.position) <= maxFlashDistance)
        {
            transform.position = dest + Vector3.up;
        }
        else
        {
            var dir = dest + Vector3.up - transform.position;
            transform.position += dir.normalized * maxFlashDistance;
        }
        player.PlaySound(1f);
        canUse = false;
        player.isMove = false;
    }

    protected void UseSkill()
    {
            if (!base.CheckSkillAvailable()) return;
        if (Input.GetKeyUp(KeyCode.E))
        {
            Flash();
        }
    }
    private IEnumerator CreateEffect()
    {
        flashParticle.transform.SetParent(null);
        flashParticle.Play();
        yield return Yields.WaitSeconds(0.2f);
        flashParticle.transform.SetParent(transform);
        flashParticle.transform.localPosition = Vector3.zero;
    }
}
