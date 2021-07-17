using UnityEngine;

public class AttackHandler : MonoBehaviour, IBufHandler
{
    // 이 함수는 유니티 스레드에서 호출되는 것이 아닌 WebSocket 스레드에서 호출됩니다.
    public void HandleBuffer(string payload)
    {
        AttackVO vo = JsonUtility.FromJson<AttackVO>(payload);

        
    }
}
