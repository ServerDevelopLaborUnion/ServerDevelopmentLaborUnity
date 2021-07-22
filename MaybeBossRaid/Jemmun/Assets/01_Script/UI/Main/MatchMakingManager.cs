using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchMakingManager : MonoBehaviour
{
    [SerializeField] private Button btnMatchMaking = null;
    [SerializeField] private GameObject[] playerStatus = new GameObject[4];

    // ���� �޼ҵ� ���� �뵵
    static private MatchMakingManager instance = null;

    // ��ġ����ŷ ���� �÷��̾��
    private int playersWaiting = 0;

    // ���� ���� ����
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


    // ��ġ����ŷ ��ư
    private void MatchMaking()
    {
        SocketClient.ConnectToServer(() => 
        {
            DataVO vo = new DataVO("matchmaking", JsonUtility.ToJson(new MatchMakingVO(0, false)));

            // ��ġ����ŷ ���¶�� ���� ǥ����
            SocketClient.Send(JsonUtility.ToJson(vo));

            btnMatchMaking.gameObject.SetActive(false);
        });


    }
}
