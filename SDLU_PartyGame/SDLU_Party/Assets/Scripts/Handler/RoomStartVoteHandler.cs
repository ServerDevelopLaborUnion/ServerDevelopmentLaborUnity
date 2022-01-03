using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomStartVoteHandler : MonoBehaviour
{
    void Start()
    {
        BufferHandler.Instance.AddHandler("RoomUserReady", data => {

            RoomUserReadyVO vo = JsonUtility.FromJson<RoomUserReadyVO>(data);

            VoteManager.Instance.SetReadyUserCount(vo.ready);
        });
    }
}
