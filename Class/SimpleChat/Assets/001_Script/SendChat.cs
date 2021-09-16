using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendChat : MonoBehaviour
{
    public InputField inputChat; // ����� �Է�
    public Button btnSend; // ������ ��ư
    public Text textBox; // �޼����� ǥ�õ� �ؽ�Ʈ
    static private Queue<string> msgQueue = new Queue<string>(); // 받은 메세지를 모두 넣어줄것

    private object lockObj = new object(); // Critical session 보호 용도

    private void Start()
    {
        btnSend.onClick.AddListener(() =>
        {
            DataVO vo = new DataVO("msg", inputChat.text);

            SocketClient.Send(JsonUtility.ToJson(vo));
        });
    }

    static public void HandleMessage(string msg)
    {
        msgQueue.Enqueue(msg);
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
