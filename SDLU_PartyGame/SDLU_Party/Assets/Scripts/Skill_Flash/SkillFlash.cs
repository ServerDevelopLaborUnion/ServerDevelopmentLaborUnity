using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFlash : SkillScript
{
    [SerializeField]
    private float maxFlashDistance = 7f;
    [SerializeField] private float skillCoolTime = 0f;
    [SerializeField] private ParticleSystem flashParticle = null;

    private void Update()
    {
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
        if (Vector3.Distance(dest, transform.position) < 7f)
        {
            transform.position = dest + Vector3.up;
        }
        else
        {
            var dir = dest + Vector3.up - transform.position;
            transform.position += dir.normalized * maxFlashDistance;
        }
        canUse = false;
        player.isMove = false;
    }

    protected override void UseSkill()
    {
        base.UseSkill();
        if (Input.GetKeyDown(KeyCode.E))
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
