using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisconnectedHandler : MonoBehaviour, IMsgHandler
{
    public void HandleMsg(string payload)
    {
        //DisconnectedVO vo = JsonUtility.FromJson<DisconnectedVO>(payload);
        //Debug.Log(vo);
        //Debug.Log(vo.socketId);
        int socketId = int.Parse(payload);
        //Debug.Log(payload);// vo가 잘못함
        //Debug.Log(socketId);
        GameManager.instance.DeletPlayer(socketId);
    }
}
