using System.Collections;
using System.Collections.Generic;
using UnityEngine;

sealed public class Player : CharactorBase
{
    private void Awake()
    {
        if(IsRemote)
        {
            return;
        }

        // TODO : add inputsystem and etc...
    }
}
