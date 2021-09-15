using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendChat : MonoBehaviour
{
    public InputField inputChat; // 사용자 입력
    public Button btnSend; // 보내기 버튼
    public Text textBox; // 메세지가 표시될 텍스트

    private void Start()
    {
        btnSend.onClick.AddListener(() =>
        {
            Send(inputChat.text);
        });
    }

    private void Update()
    {
        if (msgQueue.Count != 0)
        {
            lock (lockObj)
            {
                textBox.text = string.Concat(msgQueue.Dequeue() + "\r\n", textBox.text);
            }
        }
    }
}
