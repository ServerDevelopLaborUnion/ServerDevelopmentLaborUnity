using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitDataHandler : MonoBehaviour, IBufHandler
{
    // 이 함수는 유니티 스레드에서 호출되는 것이 아닌 WebSocket 스레드에서 호출됩니다.
    public void HandleBuffer(string payload)
    {
        PlayerDataVO data = JsonUtility.FromJson<PlayerDataVO>(payload);
        UserManager.InitPlayerData(data);
    }
}
