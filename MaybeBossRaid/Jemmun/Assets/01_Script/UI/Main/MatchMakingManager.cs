using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchMakingManager : MonoBehaviour
{
    [SerializeField] private Button btnMatchMaking = null;
    [SerializeField] private GameObject[] playerStatus = new GameObject[4];

    // 정적 메소드 접근 용도
    static private MatchMakingManager instance = null;

    // 메치메이킹 중인 플레이어들
    private int playersWaiting = 0;

    // 게임 시작 여부
    private bool bStart = false;

    private void Awake()
    {
        instance = this;
        btnMatchMaking.onClick.AddListener(MatchMaking);
    }

    private void FixedUpdate()
    {
        SetPlayerWaitingIcon();

        if (bStart)
        {
            Debug.Log("All Connected!");
        }

    }


    static public void SetMatchMakingStatus(int playersWaiting, bool bStart)
    {
        instance.playersWaiting = playersWaiting;
        instance.bStart = bStart;
    }

    private void SetPlayerWaitingIcon()
    {
        for (int i = 0; i < playerStatus.Length; ++i)
        {
            playerStatus[i].SetActive(false);
        }

        for (int i = 0; i < playersWaiting; ++i)
        {
            playerStatus[i].SetActive(true);
        }
    }


    // 메치메이킹 버튼
    private void MatchMaking()
    {
        SocketClient.ConnectToServer(() => 
        {
            DataVO vo = new DataVO("matchmaking", JsonUtility.ToJson(new MatchMakingVO(0, false)));

            // 메치메이킹 상태라는 것을 표시함
            SocketClient.Send(JsonUtility.ToJson(vo));

            btnMatchMaking.gameObject.SetActive(false);
        });


    }
}
