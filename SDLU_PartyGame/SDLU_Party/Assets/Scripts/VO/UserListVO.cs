using System;

[Serializable]
public class UserListVO
{
    public int userID;
    public string userName;

    public UserListVO(string UserName, int UserID)
    {
        this.userID = UserID;
        this.userName = UserName;
    }
}