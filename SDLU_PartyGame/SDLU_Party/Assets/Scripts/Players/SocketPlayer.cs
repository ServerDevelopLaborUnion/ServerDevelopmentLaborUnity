using UnityEngine;

public class SocketPlayer : MonoSingleton<SocketPlayer>
{
    public int ID { get; set; } // 아이디
    public string Name{ get; set; } // 이름
    public bool IsRemote { get; set; } // 원격으로 조종되는 플레이어인지
    
}