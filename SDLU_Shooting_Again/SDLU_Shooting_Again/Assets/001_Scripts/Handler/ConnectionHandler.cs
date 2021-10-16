using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 다른 유저 접속 처리

public class ConnectionHandler : MonoBehaviour
{
    [SerializeField] private GameObject connectionPlayerPrefab = null;

    private void Start()
    {
        BufferHandler.Instance.AddHandler("init", (data) => {

            int.TryParse(data,out int num);
            // GameManager.instance.playerBase.ID = num;
            // Debug.Log("플레이어 ID: " + GameManager.instance.playerBase.ID);
        });

        BufferHandler.Instance.AddHandler("connect", (data) => {
            // InitVO vo = JsonUtility.FromJson<InitVO>(data);
            // if (vo.id == GameManager.instance.playerBase.ID) { Debug.Log("자신의 접속 데이터, 무시함."); return; } // ID 같다고 전부 다 걸러버리면 좀 문제가 생겨서
            
            // CharactorBase charactor = Instantiate(connectionPlayerPrefab, Vector3.zero, Quaternion.identity).GetComponent<CharactorBase>();
            // charactor.ID = vo.id;
            // charactor.IsRemote = true;
        });
    }
}
