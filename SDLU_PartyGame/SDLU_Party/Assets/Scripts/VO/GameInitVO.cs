using System;
using System.Collections.Generic;

[Serializable]
public class GameInitVO
{
    List<PlayerGameInitDataVO> playerList;

    public GameInitVO(List<PlayerGameInitDataVO> playerList)
    {
        this.playerList = playerList;
    }
}
