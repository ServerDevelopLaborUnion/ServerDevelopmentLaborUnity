using UnityEngine;

public class RoomUserHandler : MonoBehaviour
{
    private void Start()
    {
        BufferHandler.Instance.AddHandler("RoomUserJoin", (data) => {
            RoomUserEventVO vo = JsonUtility.FromJson<RoomUserEventVO>(data);
            RoomManager.Instance.AddUser(vo.user);
        });

        BufferHandler.Instance.AddHandler("RoomUserLeave", (data) => {
            RoomUserEventVO vo = JsonUtility.FromJson<RoomUserEventVO>(data);
            RoomManager.Instance.RemoveUser(vo.user);
        });

        BufferHandler.Instance.AddHandler("RoomData", (data) => {
            RoomDataVO vo = JsonUtility.FromJson<RoomDataVO>(data);
            if(vo.userList.Count > 0) { // 이미 유저가 있는 방에 접속할 시
                for (int i = 0; i < vo.userList.Count; ++i) { // 유저를 방에 추가해줌
                    RoomManager.Instance.AddUser(vo.userList[i].UserID);
                }
            }
        });
    }
}