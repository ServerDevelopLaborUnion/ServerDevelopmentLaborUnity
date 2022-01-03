using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionHandler : MonoBehaviour
{
    private void Start() {
        BufferHandler.Instance.AddHandler("UserName", data => {
            UserListVO vo = JsonUtility.FromJson<UserListVO>(data);
            SocketPlayer.Instance.Name = vo.userName;
            SocketPlayer.Instance.ID = vo.userID;
            SocketClient.Instance.Send(new DataVO("MatchMaking", null));
        });
    }
}
