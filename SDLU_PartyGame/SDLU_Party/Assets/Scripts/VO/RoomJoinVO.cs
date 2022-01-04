using System;

[Serializable]
public class RoomJoinVO
{
    public int user;

    public RoomJoinVO(int user) => this.user = user;
}