using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStartHandler : MonoBehaviour, IBufHandler
{
    private bool signaled = false; // 윈소켓 그립읍니다.

    public void HandleBuffer(string payload)
    {
        signaled = true;   
    }

    private void FixedUpdate()
    {
        if (signaled)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Battle");
        }
    }
}
