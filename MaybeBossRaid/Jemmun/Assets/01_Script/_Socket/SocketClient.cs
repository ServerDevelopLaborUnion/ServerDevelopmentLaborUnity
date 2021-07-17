using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;


public class SocketClient : MonoBehaviour
{
    [SerializeField] private string url = "ws://localhost";
    [SerializeField] private int port = 32000; // <= ���ѽ� ���� ����� �����̿��� ��¿ �� ���� 56789�� ���Ƚ��ϴ�. �����׿�

    // ������
    private WebSocket ws;

    private void Start()
    {
        // ������ ����
        // ���߿� �ٷ� ���� �� �Ҽ��� ������ �Լ��� ����
        ConnectSocket();

        // ���� ����� �뵵�������.
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
        // ���� �޼����� DataVO �� �־����.
        DataVO vo = JsonUtility.FromJson<DataVO>(e.Data);
        // Handler���� �Ѱ����.
        BufferHandler.HandleBuffer(vo.type, vo.payload);
    }


    private void OnDestroy()
    {
        if (ws.ReadyState == WebSocketState.Connecting)
            ws.Close();
    }
}