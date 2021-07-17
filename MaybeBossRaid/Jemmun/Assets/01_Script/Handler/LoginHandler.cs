using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginHandler : MonoBehaviour, IBufHandler
{
    // 테스트 용 코드
    public void HandleBuffer(string payload)
    {
        LoginVO vo = JsonUtility.FromJson<LoginVO>(payload);
        Debug.Log(vo.id);
        Debug.Log(vo.pw);

    }
}
