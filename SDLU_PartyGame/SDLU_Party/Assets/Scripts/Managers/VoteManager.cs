using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoteManager : MonoSingleton<VoteManager>
{
    private Dictionary<string, int> voteDictionary{ get; set; } = new Dictionary<string, int>();
    public bool isVote { get; set; } = false;
    public int ReadyUserCount { get; private set; } = 0;

    public void SetReadyUserCount(bool add)
    {
        ReadyUserCount = add ? ReadyUserCount + 1 : ReadyUserCount - 1;
    }

    public void SetVoteDictionary(Dictionary<string,int> voteDictionary){
        this.voteDictionary = voteDictionary;
    }
    // public void VoteGame(int id , string gameName){
    //     if(isVote)return;   
    //     if(id == SocketPlayer.Instance.ID){
    //         isVote = true;
    //     }
    //     if(!voteDictionary.ContainsKey(gameName)){
    //         voteDictionary.Add(gameName, 0);
    //     }
    //     voteDictionary[gameName]++;
    //     SocketClient.Instance.Send(new DataVO("RoomUserVote", JsonUtility.ToJson(new RoomUserVoteVO(SocketPlayer.Instance.ID, voteDictionary))));   
    // }


    public void OnClickVote(int gameID){
        if(isVote)return;
        isVote = true;
        SocketClient.Instance.Send(new DataVO("Vote", JsonUtility.ToJson(new VoteVO(gameID))));
    }

}
