using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;

public class SocketClient : MonoBehaviour
{
    WebSocket ws;

    string addr = "ws://localhost"; // 주소
    int port = 32000; // 포트

    public InputField 사용자_입력; // 사용자 입력
    public Button btnSend; // 보내기 버튼
    public Text textBox; // 메세지가 표시될 텍스트

    private Queue<string> msgQueue = new Queue<string>(); // 받은 메세지를 모두 넣어줄것

    private object lockObj = new object(); // Critical session 보호 용도

    public Button btnNick;
    public InputField inputNick;



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
        

        btnNick.onClick.AddListener(() => {

            ws = new WebSocket($"{addr}:{port}"); // 새 웹소켓 인스턴스를 만듬
            // ws://localhost:32000

            ws.Connect();

            ws.OnMessage += (socket, e) =>
            {
                RecvData((WebSocket)socket, e);
            };

            ws.Send("클라 등장");

            string payload = JsonUtility.ToJson(new NickVO(inputNick.text));

            ws.Send(JsonUtility.ToJson(new DataVO("nickname", payload)));
        });


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

    private void Update()
    {
        if(msgQueue.Count != 0)
        {
            lock(lockObj)
            {
                textBox.text = string.Concat(msgQueue.Dequeue() + "\r\n", textBox.text);
            }
        }
    }

    private void SendMessage()
    {
        ws.Send(JsonUtility.ToJson(new DataVO("message", JsonUtility.ToJson(new ChatVO(사용자_입력.text)))));
    }



}
