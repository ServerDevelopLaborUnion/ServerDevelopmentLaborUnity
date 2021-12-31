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
            SocketClient.Instance.Connect();
            SceneManager.Instance.LoadScene("MatchMakingScene");
        });
    }
}
