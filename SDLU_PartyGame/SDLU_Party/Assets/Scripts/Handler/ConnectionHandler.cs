using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectionHandler : MonoBehaviour
{
    private void Start() {
        BufferHandler.Instance.AddHandler("UserName", data => {
            UserListVO vo = JsonUtility.FromJson<UserListVO>(data);
            Debug.Log(data);
            SocketPlayer.Instance.Name = vo.userName;
            SocketPlayer.Instance.ID = vo.userID;
            SceneManager.LoadScene("SceneVote");
            SocketClient.Instance.Send(new DataVO("MatchMaking", null));
        });
    }
}
