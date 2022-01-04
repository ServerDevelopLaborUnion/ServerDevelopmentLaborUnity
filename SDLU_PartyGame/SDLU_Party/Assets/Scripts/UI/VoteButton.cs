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
    private GameObject votePanel;
    // [SerializeField]
    // private GameObject roomPanel;

    private Image image;
    private void Start()
    {
        image = GetComponent<Image>();
        Text readyText = GetComponentInChildren<Text>();
        countText.text = string.Format("{0} / {0}", VoteManager.Instance.ReadyUserCount, RoomManager.Instance.GetUserCount());

        GetComponent<Button>().onClick.AddListener(() =>
        {
            ReadyStatus = !ReadyStatus;
            string payload = JsonUtility.ToJson(new { state = ReadyStatus });
            int userCount = RoomManager.Instance.GetUserCount();
            countText.text = string.Format("{0} / {0}", VoteManager.Instance.ReadyUserCount, userCount);
            image.color = ReadyStatus ? Color.green : Color.white;
            
            VoteManager.Instance.SetReadyUserCount(ReadyStatus);

            SocketClient.Instance.Send(new DataVO("Ready", payload));
            if (VoteManager.Instance.ReadyUserCount == userCount) //모든 인원이 레디했는가
            {
                SocketClient.Instance.Send(new DataVO("RoomStartVote", JsonUtility.ToJson(new RoomStartVoteVO(userCount))));
                votePanel.SetActive(true);
                gameObject.SetActive(false);
                //roomPanel.SetActive(false);
            }
        });
    }
}
