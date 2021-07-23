using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchMakingHandler : MonoBehaviour, IBufHandler
{
    public void HandleBuffer(string payload)
    {
        MatchMakingVO vo = JsonUtility.FromJson<MatchMakingVO>(payload);

        MatchMakingManager.SetMatchMakingStatus(vo.players, vo.start);
    }
}
