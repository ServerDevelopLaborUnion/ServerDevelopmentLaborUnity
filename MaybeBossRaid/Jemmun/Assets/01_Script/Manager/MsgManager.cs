using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MsgManager : MonoBehaviour
{
    public static MsgManager instance = null;

    private void Awake()
    {
        #region instance
        if (instance != null)
        {
            Debug.LogWarning("MsgManager: There are more than one MsgManager running at same scene");
        }
        instance = this;
        #endregion

    }




}
