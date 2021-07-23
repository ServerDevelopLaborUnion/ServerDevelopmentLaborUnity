using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ �����鿡 ���� �͵��� �� �ִ� Ŭ����
public partial class UserManager : MonoBehaviour
{
    static private UserManager instance = null;

    // ������ �������� CharactorBase �� ���� �迭
    private List<CharactorBase> players = new List<CharactorBase>();

    // ������ �� ��ǻ���� ������ �÷����ϰ��ִ� �÷��̾�
    // players �迭�� �ε���
    private int playerIndex = -1;

    #region Init, includes Awake and Start

    private void Awake()
    {
        instance = this;
        InitData();
    }

    // players �迭 �ʱ�ȭ, playerIndex ����
    private void InitData()
    {
        GameObject[] tempPlayerArr = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < tempPlayerArr.Length; ++i)
        {
            players.Add(tempPlayerArr[i].GetComponent<CharactorBase>());

            // isRemote �� false ��� ���� �÷����ϰ��ִ� ĳ����
            if (!players[i].isRemote)
            {
                playerIndex = i;
            }
        }
    }

    // ���� ���� Ŭ���� ���� �����̶� ���ư��ϴ�.
    /// <summary>
    /// �ڽ��� �÷����ϴ� ĳ������ ������ �ʱ�ȭ�մϴ�.
    /// </summary>
    /// <param name="vo">PlayerDataVO</param>
    static public void InitPlayerData(PlayerDataVO vo)
    {
        int targetIdx = -1;

        for (int i = 0; i < instance.players.Count; ++i)
        {
            if (instance.players[i].job == vo.job)
            {
                targetIdx = i;
                break;
            }
        }

        instance.players[targetIdx].hp = vo.hp;
        instance.players[targetIdx].mp = vo.mp;
        instance.players[targetIdx].id = vo.id;
    }
    #endregion

    /// <summary>
    /// id�� ���� �÷��̾ ã�� �ɴϴ�.
    /// </summary>
    /// <param name="id">ã�� �÷��̾��� id</param>
    /// <returns>ã�� �÷��̾��� CharactorBase</returns>
    static public CharactorBase GetPlayerBase(int id)
    {
        for (int i = 0; i < instance.players.Count; ++i)
        {
            if (instance.players[i].id == id)
            {
                return instance.players[i];
            }
        }

        Debug.LogError($"Cannot find requested player.\r\nID: {id}");
        return null;
    }

    /// <summary>
    /// ���� �濡 �ִ� ��� �÷��̾���� CharactorBase �� ��ȯ�մϴ�.
    /// </summary>
    /// <returns>List of CharactorBase</returns>
    static public List<CharactorBase> GetAllPlayerBase()
    {
        return instance.players;
    }

}

public partial class UserManager : MonoBehaviour
{
    // CRITICAL_SECTION �� ����� ������ �ϴ� ��������.
    public object lockObj = new object();

    // ���� ���ݴ��Ѱ�� true �� �Ǵ� ��������.
    [HideInInspector] public bool attacked = false;

    public Queue<AttackVO> atkQueue = new Queue<AttackVO>();

    // ���� ������ �� vo �� queue �� Enqueue ���ִ� �Լ�
    static public void SetAttacked(AttackVO vo)
    {
        lock (instance.lockObj)
        {
            instance.atkQueue.Enqueue(vo);
            instance.attacked = true;
        }
    }

    private void Update()
    {
        lock (lockObj)
        {
            // attacked ������ ����Ƽ ������� ������ �����忡�� �����ϱ� ����
            if (!attacked) return;
        }

        // �÷��� �뵵�� �����
        attacked = false;

        AttackVO vo;
        lock (lockObj)
        {
            // ť�� ������� �ʴٸ� Dequeue();
            vo = atkQueue.Count != 0 ? atkQueue.Dequeue() : null;
        }

        // Ÿ���� OnSkillHit ȣ����
        GetPlayerBase(vo.target).OnSkillHit(vo.skillEnum);
    }

}
