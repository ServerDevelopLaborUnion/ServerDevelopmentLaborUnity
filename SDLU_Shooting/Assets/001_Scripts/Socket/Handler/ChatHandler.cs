using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatHandler : MonoBehaviour
{
    void Start()
    {
        BufferHandler.Instance.AddHandler("msg", (msg) => {
            Chat.Instance.RecvChat(msg);
            Debug.Log(msg);
        });
    }
}
