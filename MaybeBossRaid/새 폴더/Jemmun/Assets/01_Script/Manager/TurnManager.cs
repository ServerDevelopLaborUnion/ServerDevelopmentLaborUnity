using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance;


    public    delegate void  TurnTask();
    protected List<TurnTask> turnTasks = new List<TurnTask>(); // ���� ���� �� ���� ����Ʈ �ȿ� �ִ� �Լ��� ���� �����Ŵ


    private void Awake()
    {
        // ��� üũ�� �ϳ� �� ��� �ϴµ� �����ҽ����.
        instance = this;
    }



    /// <summary>
    /// ���� ������ ȣ��Ǿ��ϴ� �Լ����� ���⿡ ���� �� �־��.
    /// </summary>
    /// <param name="task">���� ���� �� ȣ��Ǵ� �Լ�</param>
    public void AddTask(TurnTask task)
    {
        NullChecker.CheckNULL(task, true);
        turnTasks.Add(task);
    }

    

    /// <summary>
    /// ���� ���� �� ȣ��Ǵ� �Լ�
    /// </summary>
    public void EndTurn()
    {
        DoTurnEndTasks();


    }


    #region EndTurn ���� ���Ǵ� �Լ���

    private void DoTurnEndTasks()
    {
        for (int i = 0; i < turnTasks.Count; ++i)
        {
            turnTasks[i]();
        }
    }

    #endregion
}
