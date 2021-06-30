using System;

[Serializable]
public class DataVO
{
    // 서버에 데이터를 PROTOCOL:PAYLOAD 형식으로 보낼 거에요.
    
    public string protocol;
    public string payload;

    public DataVO(string protocol, string payload)
    {
        this.protocol = protocol;
        this.payload = payload;
    }
}
