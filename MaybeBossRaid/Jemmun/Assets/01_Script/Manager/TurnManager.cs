using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private Transform trmTurnIndicator = null;

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
        NullChecker.CheckNULL(trmTurnIndicator, true);

        // ��� üũ�� �ϳ� �� ��� �ϴµ� �����ҽ����.
        instance = this;

        turn = 0;
    }

    private void Start()
    {
        SocketClient.Send(JsonUtility.ToJson(new DataVO("gamestart", "")));

        playerList = UserManager.GetAllPlayerBase();
        SetTurnStatus(); // ó���� �����������...
        SetTurnIndicatorLocation();
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
        SetTurnStatus();
        SetTurnIndicatorLocation();
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
        for (int i = 0; i < playerList.Count; ++i)
        {
            playerList[i].isTurn = false;
        }

        playerList[playerList.Count == 0 ? 0 : turn % playerList.Count].isTurn = true;
    }



    // �� �ε������� ������
    private void SetTurnIndicatorLocation()
    {
        Vector2 targetPos = playerList[turn % playerList.Count].gameObject.transform.position;
        targetPos.y += 2.5f;

        trmTurnIndicator.position = targetPos;
    }

}
