using System;

[Serializable]
public class DataVO
{
    // 보내는 데이터와 받는 데이터는 모두
    // { "type": "type",  "payload": "payload" }
    // 형식으로 되어 있어요.

    // 서버랑 변수명이 다르면 작동하지 않아요.
    public string type;
    public string payload;
    
    public DataVO(string type, string payload)
    {
        this.type    = type;
        this.payload = payload;
    }
}
