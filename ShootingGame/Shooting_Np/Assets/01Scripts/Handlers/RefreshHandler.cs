using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshHandler : MonoBehaviour, IMsgHandler
{
    public void HandleMsg(string payload)
    {
        
        RefreshVO vo = JsonUtility.FromJson<RefreshVO>(payload);
        
        GameManager.instance.SetRefreshData(vo.dataList);
    }

}
