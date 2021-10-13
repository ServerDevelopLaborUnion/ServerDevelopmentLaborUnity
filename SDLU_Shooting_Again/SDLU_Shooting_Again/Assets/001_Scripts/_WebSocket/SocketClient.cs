using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class SocketClient : MonoSingleton<SocketClient>
{
    private WebSocket ws;

    public void Connect(string ip = "127.0.0.1", ushort port = 32000)
    {
        ws = new WebSocket($"ws://{ip}:{port}");

        try
        {
            ws.Connect();
        }
        catch(System.Exception e)
        {
            Debug.LogError($"Error while connecting to server.\r\n{e.Message}"); 
        }
    }

    public void Send(string msg)
    {
        ws.Send(msg);
    }
    
    
}
