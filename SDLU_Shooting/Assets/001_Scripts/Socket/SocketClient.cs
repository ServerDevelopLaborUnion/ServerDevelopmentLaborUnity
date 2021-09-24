using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

// 보내고 받는 기능이 있는 클라이언트

public class SocketClient : MonoBehaviour
{
    static SocketClient inst = null;

    private void Awake()
    {
        inst = this;

        Connect("localhost", "32000"); // TODO : Debug code
    }

    private WebSocket ws; // connect 에서 인스턴스 할것




    static public void Connect(string ip, string port) // 나중에는 json 에 있는 파일을 읽어서 ip, url 입력할 필요 없게 해봐도 좋을 듯 함
    {
        if(inst.ws != null && inst.ws.IsAlive)
        {
            Debug.Log("Already Connected to server. aborting");
            return;
        }

        inst.ws = new WebSocket($"ws://{ip}:{port}");

        inst.ws.Connect();

        inst.ws.OnMessage += (socket, e) => {
            inst.RecvData((WebSocket)socket, e);
        };

        Send(new DataVO("msg", "wa sans")); // TODO : Debug code
    }
    private void RecvData(WebSocket sender, MessageEventArgs message)
    {
        BufferHandler.Instance.Handle(message.Data);
        Debug.Log(message.Data);
    }
    
#region Send Function

    ///<summary>
    ///Sends message to server.
    ///</summary>
    static public void Send(string msg)
    {
        inst.ws.Send(msg);
    }

    ///<summary>
    ///Automaticly converts DataVO to json, and sends buffer to server.
    ///</summary>
    static public void Send(DataVO vo)
    {
        try
        {
            inst.ws.Send(JsonUtility.ToJson(vo));
        }
        catch(System.Exception e)
        {
            Debug.LogError($"서버에 메세지를 보내는 중 오류가 발생했어요.\r\n{e.Message}\r\n{e.StackTrace}");
        }
    }

#endregion

}
