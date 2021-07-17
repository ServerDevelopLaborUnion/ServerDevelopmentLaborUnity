using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BufferHandler : MonoBehaviour
{
    static private BufferHandler instance = null;

    // Ÿ�Կ� ���� �ൿ�� ����
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
        if (instance.handlerDictionary.TryGetValue(type, out handler)) // type �� ���� ���� ã�� ��� handler �� ����˴ϴ�.
        {
            handler.HandleBuffer(payload);
        }
        else // �� ã�� ���
        {
            Debug.LogWarning($"�������� �ʴ� �������� ��û {type}");
        }
    }

}
