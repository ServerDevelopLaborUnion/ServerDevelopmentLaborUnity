using System;
using System.Collections.Generic;

[Serializable]
public class UserInitListVO
{
    public List<PlayerGameInitDataVO> players;

    public UserInitListVO(List<PlayerGameInitDataVO> players)
    {
        this.players = players;
    }    
}
