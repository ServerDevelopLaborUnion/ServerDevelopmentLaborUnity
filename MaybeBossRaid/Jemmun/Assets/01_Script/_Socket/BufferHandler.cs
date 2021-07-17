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

        handlerDictionary.Add("login",    GetComponent<LoginHandler>());
        handlerDictionary.Add("attacked", GetComponent<AttackHandler>());
        handlerDictionary.Add("initdata", GetComponent<InitDataHandler>());
    }

    static public void HandleBuffer(string type, string payload)
    {
        IBufHandler handler = null;
        if (instance.handlerDictionary.TryGetValue(type, out handler)) // type 에 대한 값을 찾은 경우 handler 에 저장됩니다.
        {
            handler.HandleBuffer(payload);
        }
        else // 못 찾은 경우
        {
            Debug.LogWarning($"존재하지 않는 프로토콜 요청 {type}");
        }
    }

}
