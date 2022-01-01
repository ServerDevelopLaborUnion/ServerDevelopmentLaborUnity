using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchMakingManager : MonoSingleton<MatchMakingManager>
{
    public event Action OnUserAdded;
    [SerializeField] private Text timeTakenText;

    private bool _onMatch = true;
    public bool OnMatch {
        get {
            return _onMatch;
        }
        set {
            _onMatch = value;
        }
    }

    private List<int> _onMatchUserList = new List<int>();
    private Exception UserIdDuplicateException = new Exception("_onMatchUserList already contains handled id.");

    private float timeTaken = 0.0f; // 메치메이킹 시간

    private void Update()
    {
        if(OnMatch) {
            timeTaken += Time.deltaTime;
            timeTakenText.text = string.Format("{0}:{1} 초 경과...", (((int)timeTaken) / 60).ToString().PadLeft(2, '0'), ((int)timeTaken % 60).ToString().PadLeft(2, '0'));
        }
    }

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

    public int GetUserCount() => _onMatchUserList.Count;
    public void SetMatchMakingStatus(bool status) => OnMatch = status;

}
