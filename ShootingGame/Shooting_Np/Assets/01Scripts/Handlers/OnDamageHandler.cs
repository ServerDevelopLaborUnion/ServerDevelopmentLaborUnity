using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDamageHandler : MonoBehaviour, IMsgHandler
{
    public void HandleMsg(string payload)
    {
        OnDamageVO vo = JsonUtility.FromJson<OnDamageVO>(payload);

        GameManager.instance.SetOnDamageData(vo.socketId, vo.damage);
    }
}
