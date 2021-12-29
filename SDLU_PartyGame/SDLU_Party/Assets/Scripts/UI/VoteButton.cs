using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoteButton : MonoBehaviour
{
    public bool ReadyStatus { get; set; } = false;

    private void Start()
    {
        Text readyText = GetComponentInChildren<Text>();

        GetComponent<Button>().onClick.AddListener(() => {
            ReadyStatus = !ReadyStatus;

            string payload   = JsonUtility.ToJson(new { state = ReadyStatus });
            int    userCount = MatchMakingManager.Instance.GetUserCount();

            readyText.text = ReadyStatus ? $"{VoteManager.Instance.ReadyUserCount} / {userCount}투표하기" : 
                                           $"{VoteManager.Instance.ReadyUserCount} / {userCount}투표 취소하기";

            VoteManager.Instance.SetReadyUserCount(true);

            SocketClient.Instance.Send(new DataVO("Ready", payload));
        });
    }
}
