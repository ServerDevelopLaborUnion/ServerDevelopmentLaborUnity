using UnityEngine;

public class ChatHandler : MonoBehaviour, IMsgHandler
{
    public void HandleMsg(string payload)
    {
        Debug.Log(payload);
    }
}