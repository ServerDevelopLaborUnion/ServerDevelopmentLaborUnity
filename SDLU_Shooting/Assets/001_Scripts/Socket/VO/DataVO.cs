using System;

[Serializable]
public class DataVO
{
    public string type;
    public string payload;
    public int id;

    public DataVO(string type, int id, string payload)
    {
        this.type = type;
        this.id = id;
        this.payload = payload;
    }
}