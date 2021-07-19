using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 접속한 유저들에 관한 것들을 해 주는 클레스
public class UserManager : MonoBehaviour
{
    static private UserManager instance = null;

    // 접속한 유저들의 CharactorBase 를 가진 배열
    private CharactorBase[] players = new CharactorBase[4];

    // 실제로 이 컴퓨터의 유저가 플레이하고있는 플레이어
    // players 배열의 인덱스
    private int playerIndex = -1;

    #region Init, includes Awake and Start

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        InitData();
    }

    // players 배열 초기화, playerIndex 설정
    private void InitData()
    {
        GameObject[] tempPlayerArr = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < tempPlayerArr.Length; ++i)
        {
            players[i] = tempPlayerArr[i].GetComponent<CharactorBase>();

            // isRemote 가 false 라면 직접 플레이하고있는 캐릭터
            if (!players[i].isRemote)
            {
                playerIndex = i;
            }
        }
    }
    #endregion

    // 직접 만든 클레스 변수 수정이라 돌아갑니다.
    static public void InitPlayerData(PlayerDataVO vo)
    {
        instance.players[instance.playerIndex].hp = vo.hp;
        instance.players[instance.playerIndex].mp = vo.mp;
        instance.players[instance.playerIndex].id = vo.id;
    }
    
    /// <summary>
    /// id에 따라 플레이어를 찾아 옵니다.
    /// </summary>
    /// <param name="id">찾을 플레이어의 id</param>
    /// <returns>찾은 플레이어의 CharactorBase</returns>
    static public CharactorBase GetPlayerBase(int id)
    {
        for (int i = 0; i < instance.players.Length; ++i)
        {
            if (instance.players[i].id == id)
            {
                return instance.players[i];
            }
        }

        Debug.LogError($"Cannot find requested player.\r\nID: {id}");
        return null;
    }

}
