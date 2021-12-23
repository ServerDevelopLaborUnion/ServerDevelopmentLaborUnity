using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillScript : MonoBehaviour
{
    private float time = 0;
    protected bool canUse = true;

    protected void CoolDown(float coolTime, ref bool canUse)
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
}
