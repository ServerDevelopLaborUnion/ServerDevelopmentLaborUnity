using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BufferHandler : MonoBehaviour
{
    static public BufferHandler Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogWarning("There are more than one BufferHandler running at same scene");
        }
        Instance = this;
    }

    // 서버에서 온 메세지에 따른 Action 이 담김
    private Dictionary<string, System.Action> handlerDict = new Dictionary<string, System.Action>();

    /// <summary>
    /// 서버가 보낸 데이터를 파싱하고 dictionary 에 type 을 넘겨 줌
    /// </summary>
    /// <param name="data">DataVO based JSON string from server.</param>
    public void Handle(string data)
    {
        DataVO vo = JsonUtility.FromJson<DataVO>(data);

        if(!handlerDict.ContainsKey(vo.type))
        {
            Debug.LogError($"BufferHandler: \"type:{vo.type}\"\r\n그런 타입이 없습니다.");
            return;
        }

        // TODO : 유니티 스레드에서 handlerDict[type]?.Invoke();
    }
}
