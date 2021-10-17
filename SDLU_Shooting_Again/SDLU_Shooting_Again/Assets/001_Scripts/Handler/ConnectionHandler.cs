using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 본인의 접속 처리
// 다른 유저 접속 처리

public class ConnectionHandler : MonoBehaviour
{
    [SerializeField] private GameObject newPlayerPrefab = null;

    private void Start()
    {
        BufferHandler.Instance.AddHandler("init", (data) => {
            ConnectVO vo = JsonUtility.FromJson<ConnectVO>(data);      

            // 기본 정보 설정
            GameManager.Instance.Player.Init(vo.id, vo.hp);
            GameManager.Instance.Player.transform.position = JsonUtility.FromJson<Vector3>(vo.pos);

            UserManager.Instance.Add(vo.id, GameManager.Instance.Player);
        });

        BufferHandler.Instance.AddHandler("connect", (data) => {
            ConnectVO vo = JsonUtility.FromJson<ConnectVO>(data);

            if(GameManager.Instance.Player.ID == vo.id)
            {
                Debug.Log("자기 자신의 connect 이벤트 데이터, 무시.");
                return;
            }

            // 플레이어 프리팹 생성
            CharactorBase charactor = Instantiate(newPlayerPrefab,
                                                  JsonUtility.FromJson<Vector3>(vo.pos),
                                                  Quaternion.identity).GetComponent<CharactorBase>();

            charactor.Init(vo.id, vo.hp, true);
            UserManager.Instance.Add(vo.id, charactor);
        });
    }
}
