using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitHandler : MonoBehaviour
{
    private void Start()
    {
        BufferHandler.Instance.AddHandler("gameinit", (data) => {
            UserInitListVO vo = JsonUtility.FromJson<UserInitListVO>(data);
            for (int i = 0; i < vo.players.Count; ++i) {
                UserManager.Instance.AddUser(vo.players[i].id, vo.players[i].position, vo.players[i].rotation);
            }
        });
    }
}
