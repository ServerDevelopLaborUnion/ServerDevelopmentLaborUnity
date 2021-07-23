using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    // Singleton?
    public static TurnManager instance;

    // Task
    public delegate void  TurnTask();
    protected List<TurnTask> endTurnTasks = new List<TurnTask>(); // ���� ���� �� ���� ����Ʈ �ȿ� �ִ� �Լ��� ���� �����Ŵ
    protected List<TurnTask> midTurnTasks = new List<TurnTask>(); // ���� ���� �� ���� ����Ʈ �ȿ� �ִ� �Լ��� ���� �����Ŵ

    protected List<CharactorBase> playerList = new List<CharactorBase>();

    // Turn
    public int turn { get; private set; }

    private void Awake()
    {
        // ��� üũ�� �ϳ� �� ��� �ϴµ� �����ҽ����.
        instance = this;

        SocketClient.Send(JsonUtility.ToJson(new DataVO("gamestart", "null")));

        turn = 0;
    }

    private void Start()
    {
        playerList = UserManager.GetAllPlayerBase();
    }


    /// <summary>
    /// ���� ������ ȣ��Ǿ��ϴ� �Լ����� ���⿡ ���� �� �־��.
    /// </summary>
    /// <param name="task">���� ���� �� ȣ��Ǵ� �Լ�</param>
    public void AddEndTask(TurnTask task)
    {
        NullChecker.CheckNULL(task, true);
        endTurnTasks.Add(task);
    }

    /// <summary>
    /// �ʿ��� �� �ҷ��� �� �߰��� ����Ǵ� �Լ����� ���⿡ ���� �� �־��.
    /// </summary>
    /// <param name="task"></param>
    public void AddMidTask(TurnTask task)
    {
        NullChecker.CheckNULL(task, true);
        midTurnTasks.Add(task);
    }

    /// <summary>
    /// ���� ���� �� ȣ��Ǵ� �Լ�
    /// </summary>
    public void EndTurn()
    {
        ++turn;
        DoTurnEndTasks();
    }

    /// <summary>
    /// �ʿ��� ��� �� �߰��� ����Ǵ� �Լ�
    /// </summary>
    public void MidTurn()
    {
        DoMidTurnTasks();

        // UI �� �׷� �͵�
    }


    #region EndTurn, MidTurn Tasks

    private void DoTurnEndTasks()
    {
        // turnTasks �� ��� �ִ� ��� �Լ��� ����
        for (int i = 0; i < endTurnTasks.Count; ++i)
        {
            endTurnTasks[i]();
        }
    }

    private void DoMidTurnTasks()
    {
        for (int i = 0; i < midTurnTasks.Count; ++i)
        {
            midTurnTasks[i]();
        }
    }

    #endregion


    // ĳ���͵��� �� ���¸� ������
    private void SetTurnStatus()
    {
        // ��
    }
}
