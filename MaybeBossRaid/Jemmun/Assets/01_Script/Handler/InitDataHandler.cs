using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitDataHandler : MonoBehaviour, IBufHandler
{
    // �� �Լ��� ����Ƽ �����忡�� ȣ��Ǵ� ���� �ƴ� WebSocket �����忡�� ȣ��˴ϴ�.
    public void HandleBuffer(string payload)
    {
        PlayerDataVO data = JsonUtility.FromJson<PlayerDataVO>(payload);
        UserManager.InitPlayerData(data);
    }
}
