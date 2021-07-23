using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchMakingManager : MonoBehaviour
{
    [SerializeField] private GameObject objMatchMakingStat = null; // ��ġ����ŷ ���� ǥ��
    [SerializeField] private Text       txtMatchMakingStat = null;

    [SerializeField] private Button btnMatchMaking = null; // ��ġ����ŷ
    [SerializeField] private Button btnCancel      = null; // ��ġ����ŷ ���

    [SerializeField] private InputField ip   = null; // ���� ip
    [SerializeField] private InputField port = null; // ���� port
    
    // ��ġ����ŷ �ο��� ȿ��
    [SerializeField] private GameObject[] playerStatus = new GameObject[4];


    // ���� �޼ҵ� ���� �뵵
    static private MatchMakingManager instance = null;

    // ��ġ����ŷ ���� �÷��̾��
    private int playersWaiting = 0;

    // ���� ���� ����
    private bool bStart = false;

    // ��ġ����ŷ ����
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

        // �������� �̸� �־��
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

    // SetMatchMakingStatus �� ����Ƽ �����忡�� ������� �ʱ� ����
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
        txtMatchMakingStat.text = $"({playersWaiting} / {playerStatus.Length}) ���� ������Դϴ�.";
    }

    #endregion

    #region btnFunction

    // ��ġ����ŷ ��ư
    private void MatchMaking()
    {
        int port = -1;
        if (!int.TryParse(this.port.text, out port) && port < ushort.MaxValue && port > ushort.MinValue) // ��Ʈ�� ���ڰ� �ƴϰų� �߸��� ���Ͻ�
        {
            Debug.Log($"�߸��� port �Է�: {this.port.text}");
            return;
        }


        SocketClient.ConnectToServer(ip.text, port, () => 
        {
            DataVO vo = new DataVO("matchmaking", JsonUtility.ToJson(new MatchMakingVO(0, false)));

            // ��ġ����ŷ ���¶�� ���� ǥ����
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

    // UI enable ���� �ٲ��ִ� �Լ�
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
