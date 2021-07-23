using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobHandler : MonoBehaviour, IBufHandler
{
    public void HandleBuffer(string payload)
    {
        JobVO vo = JsonUtility.FromJson<JobVO>(payload);
        JobManager.HandleVO(vo);
    }
}
