using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class SocketClient : MonoBehaviour
{
    public string host = "ws://localhost";
    public int port = 9800;

    /// <summary>
    /// �̰Ŵ� ������ ���� �ν��Ͻ�
    /// </summary>
    private WebSocket ws; 
    
    void Start()
    {
        ws = new WebSocket($"{host}:{port}");
        ws.Connect();

        ws.OnMessage += (sender, e) =>
        {
            Debug.Log(e.Data);

        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
