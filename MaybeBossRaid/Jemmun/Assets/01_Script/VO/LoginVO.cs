using System;

[Serializable]
public class LoginVO
{
    public string id;
    public string pw;

    public LoginVO(string id, string pw)
    {
        this.id = id;
        this.pw = pw;
    }
}
