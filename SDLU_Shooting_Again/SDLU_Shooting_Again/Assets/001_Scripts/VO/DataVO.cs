using System;

[Serializable]
public class DataVO
{
    string type;
    string payload;


    public DataVO(string type, string payload)
    {
        this.type = type;
        this.payload = payload;
    }
}
