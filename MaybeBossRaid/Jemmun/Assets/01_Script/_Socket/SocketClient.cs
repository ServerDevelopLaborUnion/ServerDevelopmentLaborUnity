using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;


public class SocketClient : MonoBehaviour
{
    [SerializeField] private string url = "ws://localhost";
    [SerializeField] private int port = 32000; // <= ���ѽ� ���� ����� �����̿��� ��¿ �� ���� 56789�� ���Ƚ��ϴ�. �����׿�

    public bool instaConnect = false;

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

    private void Start()
    {
        if (instaConnect)
        {
            ConnectToServer();
        }
    }

    /// <summary>
    /// ������ �����ϴ� �Լ�
    /// </summary>
    /// <param name="callback"></param>
    static public void ConnectToServer(OnConnected callback = null)
    {
        instance.ws = new WebSocket($"{instance.url}:{instance.port}");
        instance.ws.Connect();

        instance.ws.OnMessage += (sender, e) =>
        {
            instance.ReceiveData((WebSocket)sender, e);
        };

        callback?.Invoke();
    }

    /// <summary>
    /// ������ �����ϴ� �Լ�
    /// </summary>
    /// <param name="ip">������ ip<br></br>ws://"�� �κ�":port</param>
    /// <param name="port">��Ʈ<br></br>ws://ip:"�� �κ�"</param>
    /// <param name="callback"></param>
    static public void ConnectToServer(string ip, int port, OnConnected callback = null)
    {
        instance.url = $"ws://{ip}";
        instance.port = port;

        instance.ws = new WebSocket($"{instance.url}:{instance.port}");
        instance.ws.Connect();

        instance.ws.OnMessage += (sender, e) =>
        {
            instance.ReceiveData((WebSocket)sender, e);
        };

        callback?.Invoke();
    }


    static public void DisconnectToServer(OnConnected callback = null)
    {
        instance.ws.Close();

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