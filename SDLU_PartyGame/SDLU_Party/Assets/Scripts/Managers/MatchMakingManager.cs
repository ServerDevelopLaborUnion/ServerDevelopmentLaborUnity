using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchMakingManager : MonoSingleton<MatchMakingManager>
{
    public event Action OnUserAdded;


    [SerializeField] private Text timeTakenText;
    private bool _onMatch = false;

    private List<int> _onMatchUserList = new List<int>();
    private Exception UserIdDuplicateException = new Exception("_onMatchUserList already contains handled id.");

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

    public int GetUserCount()
    {
        return _onMatchUserList.Count;
    }

    public void SetMatchMakingStatus(bool status) => _onMatch = status;

}
