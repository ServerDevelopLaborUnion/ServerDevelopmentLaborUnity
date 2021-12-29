using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchMakingManager : MonoSingleton<MatchMakingManager>
{
    Exception UserIdDuplicateException = new Exception("_onMatchUserList already contains handled id.");

    public event Action OnUserAdded;

    private List<int> _onMatchUserList = new List<int>();

    /// <summary>
    /// 유저를 메치 리스트에 추가합니다.
    /// </summary>
    /// <param name="id"></param>
    public void AddUser(int id)
    {
        if(_onMatchUserList.Contains(id)) throw UserIdDuplicateException;
        _onMatchUserList.Add(id);


        OnUserAdded();
    }

    public int GetUserCount() {
        return _onMatchUserList.Count;
    }

}
