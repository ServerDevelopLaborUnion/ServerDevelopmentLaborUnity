using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 다른 유저 접속 처리

public class ConnectionHandler : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private void Start()
    {
        BufferHandler.Instance.AddHandler("connect", (msg) => {
            // TODO : setid
            CharactorBase charactor = Instantiate(player, Vector3.zero, Quaternion.identity).GetComponent<CharactorBase>(); // TODO : 나중에 서버에서 스폰 위치 받아올 수 있음
            charactor.IsRemote = true;
        });
    }
}
