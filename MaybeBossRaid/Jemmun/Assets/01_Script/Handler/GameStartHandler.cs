using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartHandler : MonoBehaviour, IBufHandler
{
    bool bStarted = false;

    public void HandleBuffer(string payload)
    {
        bStarted = true;
    }

    private void FixedUpdate()
    {
        if (bStarted)
        {
            bStarted = false;
            UnityEngine.SceneManagement.SceneManager.LoadScene("BattlePrepare");
        }
    }
}
