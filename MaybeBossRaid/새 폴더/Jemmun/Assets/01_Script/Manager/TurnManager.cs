using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance;


    public    delegate void  TurnTask();
    protected List<TurnTask> turnTasks = new List<TurnTask>(); // 턴이 끝날 때 마다 리스트 안에 있는 함수를 전부 실행시킴


    private void Awake()
    {
        // 사실 체크를 하나 해 줘야 하는데 귀찮았스빈다.
        instance = this;
    }



    /// <summary>
    /// 턴을 끝낼때 호출되야하는 함수들을 여기에 넣을 수 있어요.
    /// </summary>
    /// <param name="task">턴이 끝날 때 호출되는 함수</param>
    public void AddTask(TurnTask task)
    {
        NullChecker.CheckNULL(task, true);
        turnTasks.Add(task);
    }

    

    /// <summary>
    /// 턴이 끝날 때 호출되는 함수
    /// </summary>
    public void EndTurn()
    {
        DoTurnEndTasks();


    }


    #region EndTurn 에서 사용되는 함수들

    private void DoTurnEndTasks()
    {
        for (int i = 0; i < turnTasks.Count; ++i)
        {
            turnTasks[i]();
        }
    }

    #endregion
}
