using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorHandler : MonoBehaviour
{
    void Start()
    {
        BufferHandler.Instance.AddHandler("errmsg", (msg) => {
            Debug.LogError($"서버에서 보내온 오류 메세지: {msg}");
        });
    }
}
