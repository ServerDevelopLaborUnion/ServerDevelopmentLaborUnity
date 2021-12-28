using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionUI : MonoBehaviour
{
    [SerializeField] private Button btnConnect;

    private void Start()
    {
        btnConnect.onClick.AddListener(() => {
            SocketClient.Instance.Connect();
        });
    }
}
