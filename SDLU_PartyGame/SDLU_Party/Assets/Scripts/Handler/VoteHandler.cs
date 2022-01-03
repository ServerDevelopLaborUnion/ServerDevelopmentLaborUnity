using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VoteHandler : MonoBehaviour
{
    private void Start() {
        BufferHandler.Instance.AddHandler("RoomStartVote", data =>
        {
            RoomStartVoteVO vo = JsonUtility.FromJson<RoomStartVoteVO>(data);
        });

        BufferHandler.Instance.AddHandler("RoomGameStart", data =>
        {
            RoomGameStartVO vo = JsonUtility.FromJson<RoomGameStartVO>(data);

            SceneManager.LoadScene(vo.gameNumber);
        });
    }
}
