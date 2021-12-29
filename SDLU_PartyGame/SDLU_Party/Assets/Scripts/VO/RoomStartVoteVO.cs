using System;

public class RoomStartVoteVO
{
    public int user;
    public bool ready;

    public RoomStartVoteVO(int user, bool ready) 
    {
        this.user = user;
        this.ready = ready;
    }
}
