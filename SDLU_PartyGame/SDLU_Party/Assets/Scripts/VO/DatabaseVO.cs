using System;

[Serializable]
public class DatabaseVO
{
    public string _type;
    public string _params;

    public DatabaseVO(string _type, string _params) 
    {
        this._type = _type;
        this._params = _params;
    }
}
