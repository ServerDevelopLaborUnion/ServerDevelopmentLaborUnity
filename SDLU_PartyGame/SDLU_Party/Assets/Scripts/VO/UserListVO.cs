using System;

[Serializable]
public class UserListVO
{
    public int UserID;
    public string UserName;

    public UserListVO(int UserID, string UserName)
    {
        this.UserID = UserID;
        this.UserName = UserName;
    }
}