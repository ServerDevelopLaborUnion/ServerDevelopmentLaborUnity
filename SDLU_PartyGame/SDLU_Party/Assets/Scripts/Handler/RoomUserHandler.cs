using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomUserHandler : MonoBehaviour
{
    [SerializeField]
    private Text countText; // 인원수 적혀있는 텍스트

    private void Start()
    {
        BufferHandler.Instance.AddHandler("JoinRoom", (data) =>
        {
            JoinRoomVO vo = JsonUtility.FromJson<JoinRoomVO>(data);
            RoomManager.Instance.RoomID = vo.id;
        });

        BufferHandler.Instance.AddHandler("RoomUserJoin", (data) =>
        {
            RoomJoinVO vo = JsonUtility.FromJson<RoomJoinVO>(data);
            RoomManager.Instance.AddUser(vo.user);
            countText.text = string.Format("{0} / {0}", VoteManager.Instance.ReadyUserCount, RoomManager.Instance.GetUserCount());
        });

        BufferHandler.Instance.AddHandler("RoomUserLeave", (data) =>
        {
            RoomJoinVO vo = JsonUtility.FromJson<RoomJoinVO>(data);
            RoomManager.Instance.RemoveUser(vo.user);
            countText.text = string.Format("{0} / {0}", VoteManager.Instance.ReadyUserCount, RoomManager.Instance.GetUserCount());

        });

        BufferHandler.Instance.AddHandler("RoomData", (data) =>
        {
            RoomDataVO vo = JsonUtility.FromJson<RoomDataVO>(data);
            if (vo.userList.Count > 0)
            { // 이미 유저가 있는 방에 접속할 시
                for (int i = 0; i < vo.userList.Count; ++i)
                { // 유저를 방에 추가해줌
                    RoomManager.Instance.AddUser(vo.userList[i].userID);
                }
            }
        });
    }
}