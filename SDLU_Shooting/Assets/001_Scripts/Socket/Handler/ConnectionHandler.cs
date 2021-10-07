using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 다른 유저 접속 처리

public class ConnectionHandler : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private void Start()
    {
        BufferHandler.Instance.AddHandler("init", (data) => {
            InitVO vo = JsonUtility.FromJson<InitVO>(data);
            GameManager.instance.playerBase.ID = vo.id;
        });

        BufferHandler.Instance.AddHandler("connect", (data) => {
            InitVO vo = JsonUtility.FromJson<InitVO>(data);
            CharactorBase charactor = Instantiate(GameManager.instance.playerPrefab, Vector3.zero, Quaternion.identity).GetComponent<CharactorBase>();
            charactor.ID = vo.id;
            charactor.IsRemote = true;
        });
    }
}
