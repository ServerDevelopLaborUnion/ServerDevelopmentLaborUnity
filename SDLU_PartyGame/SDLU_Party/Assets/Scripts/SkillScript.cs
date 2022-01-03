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
        if (!hasStarted && !canUse)
        {
            CoolDown(2);
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
            SetTimerUI(coolTime);
            return;
        }
        SetTimerUI(coolTime);
        return;
    }

    protected virtual bool CheckSkillAvailable()
    {
        if (!player.IsGround()) return false;
        if (!player.isPlaying) return false;
        if (!player.isMe) return false;
        return true;
    }

    private void SetTimerUI(float goalTime)
    {

        if(canUse)
        {
            UIManager.instance._cooltimeImage[key].color = Color.white;
        }
        else
        {
            UIManager.instance._cooltimeImage[key].color = Color.grey;
            UIManager.instance._cooltimeSlider[key].value = (time / goalTime);
        }
    }
}
