using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;


public class SocketClient : MonoBehaviour
{
    [SerializeField] private string url = "ws://localhost";
    [SerializeField] private int port = 32000; // <= 선한쌤 서버 사용할 예정이여서 어쩔 수 없이 56789를 버렸습니다. 슬프네요

    // 웹소켓
    private WebSocket ws;

    private void Start()
    {
        // 서버에 연결
        // 나중에 바로 연결 안 할수도 있으니 함수로 뺴둠
        ConnectSocket();

        // 그저 디버그 용도였스빈다.
        LoginVO login = new LoginVO("hello", "world");
        DataVO vo = new DataVO("login", JsonUtility.ToJson(login));
        ws.Send(JsonUtility.ToJson(vo));
    }

    private void ConnectSocket()
    {
        ws = new WebSocket($"{url}:{port}");
        ws.Connect();

        ws.OnMessage += (sender, e) =>
        {
            ReceiveData((WebSocket)sender, e);
        };
    }

    private void ReceiveData(WebSocket sender, MessageEventArgs e)
    {
        // 들어온 메세지를 DataVO 에 넣어줘요.
        DataVO vo = JsonUtility.FromJson<DataVO>(e.Data);
        // Handler에게 넘겨줘요.
        BufferHandler.HandleBuffer(vo.type, vo.payload);
    }


    private void OnDestroy()
    {
        if (ws.ReadyState == WebSocketState.Connecting)
            ws.Close();
    }
}