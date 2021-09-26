using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

// 보내고 받는 기능이 있는 클라이언트

public class SocketClient : MonoBehaviour
{
    static public SocketClient Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogWarning("SocketClient are running more than one in same scene");
        }
        Instance = this;

        Connect("localhost", "32000"); // TODO : Debug code
    }

    private WebSocket ws; // connect 에서 인스턴스 할것


    public void Connect(string ip, string port) // 나중에는 json 에 있는 파일을 읽어서 ip, url 입력할 필요 없게 해봐도 좋을 듯 함
    {
        if(ws != null && ws.IsAlive)
        {
            Debug.Log("Already Connected to server. aborting");
            return;
        }

        ws = new WebSocket($"ws://{ip}:{port}");

        ws.Connect();

        ws.OnMessage += (socket, e) => {
            RecvData((WebSocket)socket, e);
        };
    }
    private void RecvData(WebSocket sender, MessageEventArgs message)
    {
        Debug.Log(message.Data);
        BufferHandler.Instance.Handle(message.Data);
    }
    
    ///<summary>
    ///Automaticly converts DataVO to json, and sends buffer to server.
    ///</summary>
    public void Send(DataVO vo)
    {
        try
        {
            ws.Send(JsonUtility.ToJson(vo));
        }
        catch(System.Exception e)
        {
            Debug.LogError($"서버에 메세지를 보내는 중 오류가 발생했어요.\r\n{e.Message}\r\n{e.StackTrace}");
        }
    }

}
