using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMsgHandler
{
    public void HandleMsg(string payload);
}
