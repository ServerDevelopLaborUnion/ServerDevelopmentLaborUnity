using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendChat : MonoBehaviour
{
    public InputField inputChat; // ����� �Է�
    public Button btnSend; // ������ ��ư
    public Text textBox; // �޼����� ǥ�õ� �ؽ�Ʈ

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
