using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginHandler : MonoBehaviour, IBufHandler
{
    // �׽�Ʈ �� �ڵ�
    public void HandleBuffer(string payload)
    {
        LoginVO vo = JsonUtility.FromJson<LoginVO>(payload);
        Debug.Log(vo.id);
        Debug.Log(vo.pw);

    }
}
