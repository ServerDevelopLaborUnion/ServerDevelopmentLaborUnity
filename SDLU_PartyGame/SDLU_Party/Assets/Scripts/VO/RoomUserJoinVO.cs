using System;

[Serializable]
public class RoomUserEventVO
{
    public int user;

    public RoomUserEventVO(int user) => this.user = user;
}