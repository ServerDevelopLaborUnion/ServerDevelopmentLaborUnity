using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchMakingManager : MonoBehaviour
{
    [SerializeField] private GameObject objMatchMakingStat = null; // 메치메이킹 상태 표시
    [SerializeField] private Text       txtMatchMakingStat = null;

    [SerializeField] private Button btnMatchMaking = null; // 메치메이킹
    [SerializeField] private Button btnCancel      = null; // 메치메이킹 취소

    [SerializeField] private InputField ip   = null; // 서버 ip
    [SerializeField] private InputField port = null; // 서버 port
    
    // 메치메이킹 인원수 효과
    [SerializeField] private GameObject[] playerStatus = new GameObject[4];


    // 정적 메소드 접근 용도
    static private MatchMakingManager instance = null;

    // 메치메이킹 중인 플레이어들
    private int playersWaiting = 0;

    // 게임 시작 여부
    private bool bStart = false;

    // 메치메이킹 여부
    private bool onMatchMaking = false;

    private void Awake()
    {
        #region null check
#if UNITY_EDITOR
        NullChecker.CheckNULL(btnMatchMaking, true);
        NullChecker.CheckNULL(btnCancel, true);
        NullChecker.CheckNULL(objMatchMakingStat, true);
        NullChecker.CheckNULL(ip, true);
        NullChecker.CheckNULL(port, true);
#endif
        #endregion

        instance = this;
        btnMatchMaking.onClick.AddListener(MatchMaking);
        btnCancel.onClick.AddListener(CancelMatchMaking);

        // 귀찮으니 미리 넣어둠
        ip.text = "localhost";
        port.text = 32000.ToString();
    }

    private void FixedUpdate()
    {
        SetPlayerWaitingIcon();
        SetStatusText();


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

    #region UIFunction

    // SetMatchMakingStatus 가 유니티 스레드에서 실행되지 않기 때문
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

    private void SetStatusText()
    {
        txtMatchMakingStat.text = $"({playersWaiting} / {playerStatus.Length}) 명이 대기중입니다.";
    }

    #endregion

    #region btnFunction

    // 메치메이킹 버튼
    private void MatchMaking()
    {
        int port = -1;
        if (!int.TryParse(this.port.text, out port) && port < ushort.MaxValue && port > ushort.MinValue) // 포트가 숫자가 아니거나 잘못된 값일시
        {
            Debug.Log($"잘못된 port 입력: {this.port.text}");
            return;
        }


        SocketClient.ConnectToServer(ip.text, port, () => 
        {
            DataVO vo = new DataVO("matchmaking", JsonUtility.ToJson(new MatchMakingVO(0, false)));

            // 메치메이킹 상태라는 것을 표시함
            SocketClient.Send(JsonUtility.ToJson(vo));

            onMatchMaking = true;

            SetUIVisiblity();
        });
    }


    private void CancelMatchMaking()
    {
        DataVO vo = new DataVO("matchmaking", JsonUtility.ToJson(new MatchMakingVO(0, true)));

        SocketClient.Send(JsonUtility.ToJson(vo));

        onMatchMaking = false;

        SetUIVisiblity();

        SocketClient.DisconnectToServer();
    }

    // UI enable 상태 바꿔주는 함수
    private void SetUIVisiblity()
    {
        btnMatchMaking.gameObject.SetActive(!onMatchMaking);
        btnCancel.gameObject.SetActive(onMatchMaking);
        
        objMatchMakingStat.SetActive(onMatchMaking);

        this.ip.gameObject.SetActive(!onMatchMaking);
        this.port.gameObject.SetActive(!onMatchMaking);
        
    }

    #endregion
}
