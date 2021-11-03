using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShootManager : MonoBehaviour
{
    [SerializeField] private Transform firePos;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            SocketClient.Instance.Send(new DataVO("shoot", JsonUtility.ToJson(new ShootVO(GameManager.instance.playerBase.ID , GameManager.instance.player.eulerAngles ,firePos.position))));
        }
        // if(Input.GetKeyDown(KeyCode.Space)){
        //     Debug.Log(GameManager.instance.playerBase.ID);
        //     SocketClient.Instance.Send(new DataVO("dead" , JsonUtility.ToJson(new DeadVO(GameManager.instance.playerBase.ID))));
        //     Debug.Log("뎀지 입음");
        // }
    }
}
