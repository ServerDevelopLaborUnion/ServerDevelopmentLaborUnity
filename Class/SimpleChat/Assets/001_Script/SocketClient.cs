using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;

public class SocketClient : MonoBehaviour
{
    private static SocketClient inst;

    WebSocket ws;

    string addr = "ws://localhost"; // 주소
    int port = 32000; // 포트

    private object lockObj = new object(); // Critical session 보호 용도

    // c++
    /*
    CRITICAL_SESSION cs;
    InitalizeCriticalSession(&cs);

    EnterCriticalSession(&cs);
        ㅁㄴㅇㅁㄴㅇㅁㄴㅇ
        문맥 교환 안 일어나게 막아줌

        다음에 RAII 설명
    LeaveCriticalSession(&cs);

    DeleteCriticalSession(&cs);

    */

    private void Awake()
    {
        inst = this;
    }

    static public void Connect()
    {
        inst.ws = new WebSocket($"{inst.addr}:{inst.port}"); // 새 웹소켓 인스턴스를 만듬
        // ws://localhost:32000

        inst.ws.Connect();

        inst.ws.OnMessage += (socket, e) =>
        {
            inst.RecvData((WebSocket)socket, e);
        };

        inst.ws.Send("클라 등장");
    }

    // 웹소켓 스레드에서 돌림
    private void RecvData(WebSocket sender, MessageEventArgs message)
    {
        Debug.Log(message.Data);
        //textBox.text = message.Data;
        
        lock(lockObj)
        {
            SendChat.HandleMessage(message.Data);
        }
        
    
    }

    static public void Send(string msg)
    {
        inst.ws.Send(msg); // 대신 보내드리는 함수
    }



}
