using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Instantiate 시 GetComponent() 로 RoomNumber 설정
/// </summary>
public class RoomEnterButton : MonoBehaviour
{
    public int RoomNumber { get; set; } = -1;

    private void Awake()
    {
        if(RoomNumber == -1) {
            throw new System.Exception("RoomNumberDefaultException", new System.Exception("I guess you forgot to set RoomNumber to something not -1?"));
        }

        string payload = JsonUtility.ToJson(new { id = RoomNumber });

        GetComponent<Button>().onClick.AddListener(() => {
            SocketClient.Instance.Send(new DataVO("JoinRoom", payload));
        });
    }
}
