using System;

[Serializable]
public class DataVO
{
    // ������ �����͸� PROTOCOL:PAYLOAD �������� ���� �ſ���.
    
    public string protocol;
    public string payload;

    public DataVO(string protocol, string payload)
    {
        this.protocol = protocol;
        this.payload = payload;
    }
}
