using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SceneManagement;

public class ConnectionUI : MonoBehaviour
{
    [SerializeField] private Button btnConnect;

    private void Start()
    {
        btnConnect.onClick.AddListener(() => {
            SocketClient.Instance.Connect("127.0.0.1", 32000);
            SceneManager.Instance.LoadScene("MatchMakingScene");
        });
    }
}
