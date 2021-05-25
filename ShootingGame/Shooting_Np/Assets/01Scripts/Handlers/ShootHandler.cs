using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootHandler : MonoBehaviour, IMsgHandler
{
    public void HandleMsg(string payload)
    {
        ShootVO vo = JsonUtility.FromJson<ShootVO>(payload);

        GameManager.instance.SetShootData(vo.socketId, vo.rotation);
    }
}
