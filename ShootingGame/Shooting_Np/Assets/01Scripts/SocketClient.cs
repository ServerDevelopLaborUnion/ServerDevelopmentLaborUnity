using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using System.Reflection;

public class SocketClient : MonoBehaviour
{
    public string url = "ws://localhost";
    public int port = 32000;
    public GameObject gameManager;

    private Dictionary<string, IMsgHandler> handlerDictionary;

    private string nickname;
    private WebSocket webSocket;

    public static SocketClient instance;

    void Awake()
    {
        handlerDictionary = new Dictionary<string, IMsgHandler>();
        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    void Start()
    {
        //각 핸들러 추가
        handlerDictionary.Add("Chat", gameManager.GetComponent<ChatHandler>());
        handlerDictionary.Add("Login", gameManager.GetComponent<LoginHandler>());
        handlerDictionary.Add("Refresh", gameManager.GetComponent<RefreshHandler>());

        webSocket = new WebSocket($"{url}:{port}");
        webSocket.Connect();
        
        webSocket.OnMessage += (sender, e) => {
            ReceiveData((WebSocket)sender, e);
        };
    }

    private void ReceiveData(WebSocket sender, MessageEventArgs e){
        DataVO vo = JsonUtility.FromJson<DataVO>(e.Data);
        
        IMsgHandler handler = handlerDictionary[vo.type];
        
        handler.HandleMsg(vo.payload);
        

        //굳이 리플렉션 안써도 딕셔너리로 충분히 해결가능
        // MethodInfo m = this.GetType().GetMethod($"{vo.type}Handler", BindingFlags.Instance | BindingFlags.NonPublic);

        // if(m == null){
        //     Debug.Log("에러 발생 정의되지 않은 타입");
        // }

        // m.Invoke(this, new object[]{vo.payload});
    }

    public void SendData(string json){
        webSocket.Send(json);
    }

    void OnDestroy()
    {
        webSocket.Close();
    }

}
