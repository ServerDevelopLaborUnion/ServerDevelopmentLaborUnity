using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ �����鿡 ���� �͵��� �� �ִ� Ŭ����
public class UserManager : MonoBehaviour
{
    static private UserManager instance = null;

    // ������ �������� CharactorBase �� ���� �迭
    private CharactorBase[] players = new CharactorBase[4];

    // ������ �� ��ǻ���� ������ �÷����ϰ��ִ� �÷��̾�
    // players �迭�� �ε���
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

    // players �迭 �ʱ�ȭ, playerIndex ����
    private void InitData()
    {
        GameObject[] tempPlayerArr = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < tempPlayerArr.Length; ++i)
        {
            players[i] = tempPlayerArr[i].GetComponent<CharactorBase>();

            // isRemote �� false ��� ���� �÷����ϰ��ִ� ĳ����
            if (!players[i].isRemote)
            {
                playerIndex = i;
            }
        }
    }
    #endregion

    // ���� ���� Ŭ���� ���� �����̶� ���ư��ϴ�.
    static public void InitPlayerData(PlayerDataVO vo)
    {
        instance.players[instance.playerIndex].hp = vo.hp;
        instance.players[instance.playerIndex].mp = vo.mp;
        instance.players[instance.playerIndex].id = vo.id;
    }
    
    /// <summary>
    /// id�� ���� �÷��̾ ã�� �ɴϴ�.
    /// </summary>
    /// <param name="id">ã�� �÷��̾��� id</param>
    /// <returns>ã�� �÷��̾��� CharactorBase</returns>
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
