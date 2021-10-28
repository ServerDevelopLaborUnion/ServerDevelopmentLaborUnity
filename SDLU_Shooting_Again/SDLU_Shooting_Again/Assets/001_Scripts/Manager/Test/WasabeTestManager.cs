using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WasabeTestManager : MonoBehaviour
{
    private void Update() {
        if(Input.GetButtonDown("Jump")){
            SocketClient.Instance.Send(new DataVO("dead", JsonUtility.ToJson(new DeadVO(GameManager.Instance.Player.ID))));
        }
    }
}
