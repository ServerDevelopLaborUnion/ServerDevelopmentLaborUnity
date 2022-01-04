using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoSingleton<RoomManager>
{
    /// <summary>
    /// 방에 접속한 유저와 레디 상태
    /// </summary>
    private Dictionary<int, bool> userDictionary = new Dictionary<int, bool>();

    public int RoomID { get; set; } = -1;


    #region 외부 공개 함수

    
    /// <summary>
    /// 유저를 방에 추가합니다.
    /// </summary>
    /// <param name="id">추가할 유저의 id</param>
    /// <returns>false when fail</returns>
    public bool AddUser(int id)
    {
        if(userDictionary.ContainsKey(id)) {
            return false;
        }
        userDictionary.Add(id, false);
        return true;
    }
    
    /// <summary>
    /// 유저의 레디상태를 바꾸어줍니다
    /// </summary>
    /// <param name="id">바꿀 유저의 id</param>
    public void ChangeUser(int id , bool state){
        userDictionary[id] = state;
    }



    /// <summary>
    /// 유저를 방에서 제거합니다.
    /// </summary>
    /// <param name="id">제거할 유저의 id</param>
    /// <returns>false when fail</returns>
    public bool RemoveUser(int id)
    {
        if (!userDictionary.ContainsKey(id)) {
            return false;
        }
        userDictionary.Remove(id);
        return true;
    }

    /// <summary>
    /// 방에 접속중인 유저 수를 가져옵니다.
    /// </summary>
    public int GetUserCount()
    {
        return userDictionary.Count;
    }

    

    #endregion
}