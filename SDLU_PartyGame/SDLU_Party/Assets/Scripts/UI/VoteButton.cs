using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoteButton : MonoBehaviour
{
    public bool ReadyStatus { get; set; } = false;

    [SerializeField]
    private Text countText;
    [SerializeField]
    private CanvasGroup voteCanvasGroup;

    private Image image;
    private void Start()
    {
        image = GetComponent<Image>();
        Text readyText = GetComponentInChildren<Text>();
        countText.text = string.Format("{0} / {0}", VoteManager.Instance.ReadyUserCount, RoomManager.Instance.GetUserCount());

        GetComponent<Button>().onClick.AddListener(() =>
        {
            //if(!MatchMakingManager.Instance.OnMatch) return;

            ReadyStatus = !ReadyStatus;

            string payload = JsonUtility.ToJson(new { state = ReadyStatus });
            int userCount = RoomManager.Instance.GetUserCount();
            countText.text = string.Format("{0} / {0}", VoteManager.Instance.ReadyUserCount, userCount);
            image.color = ReadyStatus ? Color.green : Color.white;
            // readyText.text = ReadyStatus ? $"{VoteManager.Instance.ReadyUserCount} / {userCount}\r\n투표하기" : 
            //                                $"{VoteManager.Instance.ReadyUserCount} / {userCount}\r\n투표 취소하기";

            VoteManager.Instance.SetReadyUserCount(ReadyStatus);

            SocketClient.Instance.Send(new DataVO("Ready", payload));
            if (VoteManager.Instance.ReadyUserCount == userCount)
            {
                SocketClient.Instance.Send(new DataVO("RoomStartVote", JsonUtility.ToJson(new RoomStartVoteVO(userCount))));
                voteCanvasGroup.gameObject.SetActive(true);
            }
            else
            {
                voteCanvasGroup.gameObject.SetActive(false);
            }
        });
    }
}
