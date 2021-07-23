using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 접속한 유저들에 관한 것들을 해 주는 클레스
public partial class UserManager : MonoBehaviour
{
    static private UserManager instance = null;

    // 접속한 유저들의 CharactorBase 를 가진 배열
    private List<CharactorBase> players = new List<CharactorBase>();

    // 실제로 이 컴퓨터의 유저가 플레이하고있는 플레이어
    // players 배열의 인덱스
    private int playerIndex = -1;

    #region Init, includes Awake and Start

    private void Awake()
    {
        instance = this;
        InitData();
    }

    // players 배열 초기화, playerIndex 설정
    private void InitData()
    {
        GameObject[] tempPlayerArr = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < tempPlayerArr.Length; ++i)
        {
            players.Add(tempPlayerArr[i].GetComponent<CharactorBase>());

            // isRemote 가 false 라면 직접 플레이하고있는 캐릭터
            if (!players[i].isRemote)
            {
                playerIndex = i;
            }
        }
    }

    // 직접 만든 클레스 변수 수정이라 돌아갑니다.
    /// <summary>
    /// 자신이 플레이하는 캐릭터의 변수를 초기화합니다.
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
    /// id에 따라 플레이어를 찾아 옵니다.
    /// </summary>
    /// <param name="id">찾을 플레이어의 id</param>
    /// <returns>찾은 플레이어의 CharactorBase</returns>
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
    /// 같은 방에 있는 모든 플레이어들의 CharactorBase 를 반환합니다.
    /// </summary>
    /// <returns>List of CharactorBase</returns>
    static public List<CharactorBase> GetAllPlayerBase()
    {
        return instance.players;
    }

}

public partial class UserManager : MonoBehaviour
{
    // CRITICAL_SECTION 과 비슷한 역할을 하는 변수에요.
    public object lockObj = new object();

    // 만약 공격당한경우 true 가 되는 변수에요.
    [HideInInspector] public bool attacked = false;

    public Queue<AttackVO> atkQueue = new Queue<AttackVO>();

    // 공격 당했을 시 vo 를 queue 에 Enqueue 해주는 함수
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
            // attacked 변수는 유니티 스레드와 웹소켓 스레드에서 접근하기 때문
            if (!attacked) return;
        }

        // 플래그 용도로 사용함
        attacked = false;

        AttackVO vo;
        lock (lockObj)
        {
            // 큐가 비어있지 않다면 Dequeue();
            vo = atkQueue.Count != 0 ? atkQueue.Dequeue() : null;
        }

        // 타깃의 OnSkillHit 호출함
        GetPlayerBase(vo.target).OnSkillHit(vo.skillEnum);
    }

}
