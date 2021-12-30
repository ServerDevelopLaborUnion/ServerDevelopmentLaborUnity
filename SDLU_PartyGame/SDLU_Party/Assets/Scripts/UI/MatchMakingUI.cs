using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchMakingUI : MonoBehaviour
{
    [SerializeField] private Button btnMatch;
    [SerializeField] private Text text;


    private void Start()
    {
        // TODO : 이거

        // text.text = onMatchmaking ? "메치메이킹 중지하기" : "메치메이킹 시작하기";

        btnMatch.onClick.AddListener(() => {
            // DataVO vo = new DataVO(onMatchmaking ? "MatchMa")
            // text.text = onMatchmaking ? "메치메이킹 중지하기" : "메치메이킹 시작하기";
            SocketClient.Instance.Send(new DataVO("MatchMaking", JsonUtility.ToJson(new { })));
        });
    }
}
