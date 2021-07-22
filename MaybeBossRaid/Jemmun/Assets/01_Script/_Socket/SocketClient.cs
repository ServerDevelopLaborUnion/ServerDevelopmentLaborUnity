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

    // static ���� ��
    static private SocketClient instance = null;

    // ���� �Ǿ��� �� �ݹ� �Լ� ��
    public delegate void OnConnected();

    private void Awake()
    {
        // �̱��� ���� �뵵�� �ƴϿ���.
        instance = this;
    }

    //private void Start()
    //{
    //    // ������ ����
    //    // ���߿� �ٷ� ���� �� �Ҽ��� ������ �Լ��� ����
    //    ConnectSocket();
    //}

    static public void ConnectToServer(OnConnected callback)
    {
        instance.ws = new WebSocket($"{instance.url}:{instance.port}");
        instance.ws.Connect();

        instance.ws.OnMessage += (sender, e) =>
        {
            instance.ReceiveData((WebSocket)sender, e);
        };

        callback?.Invoke();
    }

    private void ReceiveData(WebSocket sender, MessageEventArgs e)
    {
        // ���� �޼����� DataVO �� �־����.
        DataVO vo = JsonUtility.FromJson<DataVO>(e.Data);
        // Handler���� �Ѱ����.
        BufferHandler.HandleBuffer(vo.type, vo.payload);
    }

    /// <summary>
    /// ������ �޼����� ������ �Լ�
    /// </summary>
    /// <param name="data">����</param>
    static public void Send(string data)
    {
        try
        {
            instance.ws.Send(data);
        }
        catch (Exception e)
        {
            Debug.LogError($"������ �����͸� ������ �� ������ ������.\r\n{e.Message}");
        }
    }

    private void OnDestroy()
    {
        if (ws.ReadyState == WebSocketState.Connecting)
            ws.Close();
    }
}