using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShootManager : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            SocketClient.Instance.Send(new DataVO("shoot", JsonUtility.ToJson(new ShootVO(GameManager.instance.playerBase.ID , GameManager.instance.player.eulerAngles ,GameManager.instance.player.position))));
        }
    }
}
