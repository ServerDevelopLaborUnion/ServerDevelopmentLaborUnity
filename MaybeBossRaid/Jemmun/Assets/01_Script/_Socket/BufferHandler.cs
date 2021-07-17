using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BufferHandler : MonoBehaviour
{
    static private BufferHandler instance = null;

    // 타입에 따른 행동을 담음
    private Dictionary<string, IBufHandler> handlerDictionary = new Dictionary<string, IBufHandler>();

    private void Awake()
    {
        instance = this;
        handlerDictionary.Add("login", GetComponent<LoginHandler>()); // ? 뭔가가 잘못됬
    }

    void Start()
    {
    }

    static public void HandleBuffer(string type, string payload)
    {
        IBufHandler handler = null;
        if (instance.handlerDictionary.TryGetValue(type, out handler))
        {
            handler.HandleBuffer(payload);
        }
        else
        {
            Debug.LogWarning($"존재하지 않는 프로토콜 요청 {type}");
        }
    }

}
