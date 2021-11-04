using System;
using UnityEngine;

[Serializable]
public class ConnectVO
{
    public int id; // 소켓 id
    public string pos; // 생성 위치, JSON 으로 파싱 해야 함
    public int hp; // 기본 hp
    public string history;

    public ConnectVO(int id, string pos, int hp, string history)
    {
        this.id = id;
        this.pos = pos;
        this.hp = hp;
        this.history = history;
    }
}
