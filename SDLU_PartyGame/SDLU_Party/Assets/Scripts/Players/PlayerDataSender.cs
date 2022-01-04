using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataSender : MonoBehaviour
{
    private void FixedUpdate()
    {
        string payload = JsonUtility.ToJson(new { id = SocketPlayer.Instance.ID, position = transform.position, rotation = transform.rotation });
        SocketClient.Instance.Send(new DataVO("move", payload));
    }
}
