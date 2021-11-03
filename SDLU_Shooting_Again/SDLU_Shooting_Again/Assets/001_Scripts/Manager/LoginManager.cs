using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginManager : MonoSingleton<LoginManager>
{
    private bool hasLogined = false;

    public bool HasLogined()
    {
        return hasLogined;
    }

    public void GotLogined()
    {
        hasLogined = true;
    }
}
