using System;

[Serializable]
public class DataVO
{
    // ������ �����Ϳ� �޴� �����ʹ� ���
    // { "type": "type",  "payload": "payload" }
    // �������� �Ǿ� �־��.

    // ������ �������� �ٸ��� �۵����� �ʾƿ�.
    public string type;
    public string payload;
    
    public DataVO(string type, string payload)
    {
        this.type    = type;
        this.payload = payload;
    }
}
