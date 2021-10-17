using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : MonoSingleton<UserManager>
{
    // value 로 캐릭터를 가지고 있는 Dictionary
    // socket.sessionId 를 key 로 가짐
    private Dictionary<int, CharactorBase> userDict = new Dictionary<int, CharactorBase>();

    /// <summary>
    /// user 를 dictionary 에 추가합니다.
    /// </summary>
    /// <param name="user">추가할 유저</param>
    /// <param name="id">추가할 유저의 id</param>
    public void Add(int id, CharactorBase user)
    {
        if(userDict.ContainsKey(id))
        {
            Debug.LogError($"UserManager > {id}: 이미 존재하는 Key.");
            return;
        }

        userDict.Add(id, user);
    }

    /// <summary>
    /// 해당되는 id 의 Charactor 를 가져옵니다.
    /// </summary>
    /// <param name="id">가져올 유저의 id</param>
    /// <returns>null when key does not exist</returns>
    public CharactorBase Get(int id)
    {
        return userDict.ContainsKey(id) ? userDict[id] : null;
    }

    /// <summary>
    /// 해당되는 id 의 유저를 제거합니다.
    /// </summary>
    /// <param name="id"></param>
    public void Remove(int id)
    {
        if(!userDict.ContainsKey(id))
        {
            Debug.LogError($"UserManager > {id}: 존재하지 않는 Key");
            return;
        }

        userDict.Remove(id);
    }
    
    /// <summary>
    /// 해당하는 id 가 이미 등록되어 있는지 확인합니다.
    /// </summary>
    /// <param name="id">확인할 id</param>
    /// <returns>true when already added</returns>
    public bool Contains(int id)
    {
        return userDict.ContainsKey(id);
    }
}
