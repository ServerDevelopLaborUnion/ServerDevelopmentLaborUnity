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
            UnityEngine.SceneManagement.SceneManager.LoadScene("Battle");
        }
    }
}
