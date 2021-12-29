using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillScript : MonoBehaviour
{
    private float time = 0;
    protected bool canUse = false;
    protected ChickenPlayer player = null;

    private IEnumerator Start()
    {
        player = GetComponent<ChickenPlayer>();
        yield return Yields.WaitSeconds(2);
        canUse = true;
    }


    protected void CoolDown(float coolTime)
    {
        if (canUse) return;

        time += Time.deltaTime;
        if (time >= coolTime)
        {
            time = 0;
            canUse = true;
            return;
        }

        return; 
    }

    protected virtual void UseSkill()
    {
        if (!player.IsGround()) return;
    }
}
