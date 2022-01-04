using System;

[Serializable]
public class RoomUserJoinVO
{
    public int user;

    public RoomUserJoinVO(int user) => this.user = user;
}