using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatHandler : MonoBehaviour
{
    void Start()
    {
        BufferHandler.Instance.AddHandler("msg", (msg) => {
            DataVO vo = JsonUtility.FromJson<DataVO>(msg);
            Chat.Instance.CreateChatPref("other", vo.payload);
            Debug.Log(msg);
        });
    }
}
