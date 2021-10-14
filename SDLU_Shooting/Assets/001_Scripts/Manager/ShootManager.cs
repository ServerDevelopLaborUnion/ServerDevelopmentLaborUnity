using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShootManager : MonoBehaviour
{
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            SocketClient.Instance.Send(new DataVO("shoot", JsonUtility.ToJson(new ShootVO(GameManager.instance.playerBase.ID , GameManager.instance.player.eulerAngles ,GameManager.instance.player.position))));
        }   
    }
}
