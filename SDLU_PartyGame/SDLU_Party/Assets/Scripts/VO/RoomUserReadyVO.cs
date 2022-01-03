using System;

public class RoomUserReadyVO
{
    public int user;
    public bool ready;

    public RoomUserReadyVO(int user, bool ready) 
    {
        this.user = user;
        this.ready = ready;
    }
}
