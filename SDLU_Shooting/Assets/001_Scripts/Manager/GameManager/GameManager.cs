using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager instance = null; // singleton

    public Transform player; // 다른 스크립트가 플레이어의 transform 을 필요로 하는 경우가 있어서
    public Rigidbody playerRigid; // 이도 위와 같음

    private void Awake()
    {
        instance = this; // singleton
    }
}
