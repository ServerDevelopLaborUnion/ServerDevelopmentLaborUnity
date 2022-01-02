using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillScript : MonoBehaviour
{
    private float time = 0;
    protected bool canUse = false;
    protected ChickenPlayer player = null;
    protected bool hasStarted = false;

    private void Start()
    {
        player = GetComponent<ChickenPlayer>();
    }

    protected virtual void Update()
    {
        if (!hasStarted)
        {
            CoolDown(2);
            Debug.Log(1);
        }
    }

    protected void CoolDown(float coolTime)
    {
        if (canUse) return;

        time += Time.deltaTime;
        if (time >= coolTime)
        {
            time = 0;
            canUse = true;
            hasStarted = true;
            return;
        }
        
        return;
    }

    protected virtual bool CheckSkillAvailable()
    {
        if (!player.IsGround()) return false;
        if (!player.isPlaying) return false;
        return true;
    }
}
