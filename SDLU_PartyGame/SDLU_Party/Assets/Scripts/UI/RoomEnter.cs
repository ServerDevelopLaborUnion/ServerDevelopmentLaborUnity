using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomEnter : MonoBehaviour
{
    [SerializeField]
    private Text roomText;
    [SerializeField]
    private Text nameText;

    private void Start() {
        nameText.text = string .Format("{0}님", SocketPlayer.Instance.Name);
        roomText.text = string.Format("{0}번 방에 오신 것을 환영합니다.", RoomManager.Instance.RoomID);
    }
}
