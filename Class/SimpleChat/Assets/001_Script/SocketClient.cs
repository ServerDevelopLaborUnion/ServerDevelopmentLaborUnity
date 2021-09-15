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



    private Queue<string> msgQueue = new Queue<string>(); // 받은 메세지를 모두 넣어줄것

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

        ws = new WebSocket($"{addr}:{port}"); // 새 웹소켓 인스턴스를 만듬
        // ws://localhost:32000

        ws.Connect();

        ws.OnMessage += (socket, e) =>
        {
            RecvData((WebSocket)socket, e);
        };

        ws.Send("클라 등장");

        btnSend.onClick.AddListener(SendMessage);
    }

    // 웹소켓 스레드에서 돌림
    private void RecvData(WebSocket sender, MessageEventArgs message)
    {
        Debug.Log(message.Data);
        //textBox.text = message.Data;
        
        lock(lockObj)
        {
            msgQueue.Enqueue(message.Data);
        }
        
    
    }

    static public void Send(string msg)
    {
        inst.ws.Send(msg); // 대신 보내드리는 함수
    }



}
