using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchMakingUI : MonoBehaviour
{
    [SerializeField] private Button btnMatch;

    private void Start()
    {
        btnMatch.onClick.AddListener(() => {
            SocketClient.Instance.Send(new DataVO("MatchMaking", ""));
        });
    }
}
