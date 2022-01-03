using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillScript : MonoBehaviour
{
    private float time = 0;
    protected bool canUse = false;
    protected ChickenPlayer player = null;
    protected bool hasStarted = false;

    protected Rigidbody rb;
    protected int key;
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = gameObject.GetComponent<ChickenPlayer>();
        Debug.Log(player.gameObject.name);
    }

    protected virtual void Update()
    {
        if (!hasStarted)
        {
            CoolDown(2);
        }
    }

    protected void CoolDown(float coolTime)
    {
        if (canUse) return;

        time += Time.deltaTime;
        SetTimerUI(coolTime);
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

    private void SetTimerUI(float goalTime)
    {
        UIManager.instance._cooltimeText[key].value = (time / goalTime);
    }
}
