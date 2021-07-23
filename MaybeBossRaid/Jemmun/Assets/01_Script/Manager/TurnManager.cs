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
    protected List<TurnTask> endTurnTasks = new List<TurnTask>(); // 턴이 끝날 때 마다 리스트 안에 있는 함수를 전부 실행시킴
    protected List<TurnTask> midTurnTasks = new List<TurnTask>(); // 턴이 끝날 때 마다 리스트 안에 있는 함수를 전부 실행시킴

    protected List<CharactorBase> playerList = new List<CharactorBase>();

    // Turn
    public int turn { get; private set; }

    private void Awake()
    {
        NullChecker.CheckNULL(trmTurnIndicator, true);

        // 사실 체크를 하나 해 줘야 하는데 귀찮았스빈다.
        instance = this;

        turn = 0;
    }

    private void Start()
    {
        SocketClient.Send(JsonUtility.ToJson(new DataVO("gamestart", "")));

        playerList = UserManager.GetAllPlayerBase();
        SetTurnStatus(); // 처음에 설정해줘야죠...
        SetTurnIndicatorLocation();
    }


    /// <summary>
    /// 턴을 끝낼때 호출되야하는 함수들을 여기에 넣을 수 있어요.
    /// </summary>
    /// <param name="task">턴이 끝날 때 호출되는 함수</param>
    public void AddEndTask(TurnTask task)
    {
        NullChecker.CheckNULL(task, true);
        endTurnTasks.Add(task);
    }

    /// <summary>
    /// 필요할 때 불려서 턴 중간에 실행되는 함수들을 여기에 넣을 수 있어요.
    /// </summary>
    /// <param name="task"></param>
    public void AddMidTask(TurnTask task)
    {
        NullChecker.CheckNULL(task, true);
        midTurnTasks.Add(task);
    }

    /// <summary>
    /// 턴이 끝날 때 호출되는 함수
    /// </summary>
    public void EndTurn()
    {
        ++turn;
        SetTurnStatus();
        SetTurnIndicatorLocation();
        DoTurnEndTasks();
    }

    /// <summary>
    /// 필요한 경우 턴 중간에 실행되는 함수
    /// </summary>
    public void MidTurn()
    {
        DoMidTurnTasks();

        // UI 나 그런 것들
    }


    #region EndTurn, MidTurn Tasks

    private void DoTurnEndTasks()
    {
        // turnTasks 에 들어 있는 모든 함수를 돌림
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


    // 캐릭터들의 턴 상태를 설정함
    private void SetTurnStatus()
    {
        for (int i = 0; i < playerList.Count; ++i)
        {
            playerList[i].isTurn = false;
        }

        playerList[playerList.Count == 0 ? 0 : turn % playerList.Count].isTurn = true;
    }



    // 턴 인디케이터 움직임
    private void SetTurnIndicatorLocation()
    {
        Vector2 targetPos = playerList[turn % playerList.Count].gameObject.transform.position;
        targetPos.y += 2.5f;

        trmTurnIndicator.position = targetPos;
    }

}
