using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager instance = null; // singleton

    // TODO : 외부에서 수정이 가능한
    public Transform player; // 다른 스크립트가 플레이어의 transform 을 필요로 하는 경우가 있어서
    public CharactorBase playerBase; // 플레이어 베이스
    public Rigidbody playerRigid; // 이도 위와 같음
    public BulletPool bulletPool; //게임메니저가 불렛 풀 알게 함
    public GameObject playerPrefab = null;

    private void Awake()
    {
        instance = this; // singleton
    }
}
