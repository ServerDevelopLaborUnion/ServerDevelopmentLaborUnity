using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendNickname : MonoBehaviour
{
    public InputField nickname;
    public Button btnSend;

    private void Start()
    {
        btnSend.onClick.AddListener(() =>
        {
            SocketClient.Connect();

            DataVO vo = new DataVO("nickname", nickname.text);

            SocketClient.Send(JsonUtility.ToJson(vo));
        });
    }

}


/*

send("chat");


json =>
send("{type:type, payload:payload}");

class Msg
{
    string type;
    string payload;
}


*/