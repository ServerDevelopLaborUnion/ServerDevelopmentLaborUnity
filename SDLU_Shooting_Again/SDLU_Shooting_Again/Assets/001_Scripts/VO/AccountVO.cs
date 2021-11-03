using System;

[Serializable]
public class AccountVO
{
    public string id;
    public string password;

    public AccountVO(string id, string password)
    {
        this.id = id;
        this.password = password;
    }
}
