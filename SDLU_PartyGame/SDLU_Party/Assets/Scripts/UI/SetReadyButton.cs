using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetReadyButton : MonoBehaviour
{
    public bool ReadyStatus { get; set; } = false;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => {
            ReadyStatus = !ReadyStatus;
            string payload = JsonUtility.ToJson(new { state = ReadyStatus });
            SocketClient.Instance.Send(new DataVO("Ready", payload));
        });
    }
}
