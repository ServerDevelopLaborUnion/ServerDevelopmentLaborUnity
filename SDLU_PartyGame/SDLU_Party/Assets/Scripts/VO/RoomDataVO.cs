using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RoomDataVO
{
    public int RoomID;
    public string RoomName;
    public List<UserListVO> userList;

    public RoomDataVO(int RoomID, string RoomName, List<UserListVO> userList)
    {
        this.RoomID = RoomID;
        this.RoomName = RoomName;
        this.userList = userList;
    }
}
