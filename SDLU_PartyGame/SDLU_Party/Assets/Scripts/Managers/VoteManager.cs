using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoteManager : MonoSingleton<VoteManager>
{
    //TODO: VOTE Count 받아서 구현
    private bool isVote { get; set; } = false;
    // public int UserCount { get; private set; } = 0;
    public int ReadyUserCount { get; private set; } = 0;
    //public int LastMenStandingVotedCount { get; private set; } = 0;

    // public void SetUserCount(bool add)
    // {
    //     UserCount = add ? UserCount + 1 : UserCount - 1;
    // }

    public void SetReadyUserCount(bool add)
    {
        ReadyUserCount = add ? ReadyUserCount + 1 : ReadyUserCount - 1;
    }

    // public void SetLastMenStandingVotedCount(bool add)
    // {
    //     LastMenStandingVotedCount = add ? LastMenStandingVotedCount + 1 : LastMenStandingVotedCount - 1;
    // }
}
