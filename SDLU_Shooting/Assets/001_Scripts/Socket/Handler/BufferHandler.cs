using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BufferHandler : MonoBehaviour
{
    static public BufferHandler Instance { get; private set; }
    private Queue<DataVO> receivedVOQueue = new Queue<DataVO>(); // 유니티 스레드에서 돌리기 위해
    private object lockObj = new object(); // lock();

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogWarning("There are more than one BufferHandler running at same scene");
        }
        Instance = this;
    }

    // 서버에서 온 메세지에 따른 Action 이 담김
    private Dictionary<string, System.Action<string>> handlerDict = new Dictionary<string, System.Action<string>>();

    /// <summary>
    /// 서버에서 메세지를 보냈을 때 type 에 따라 실행될 action 
    /// </summary>
    /// <param name="type">이벤트 타입</param>
    /// <param name="action">타입에 따른 Action. payload 가 매개변수로 전달됨</param>
    public void AddHandler(string type, System.Action<string> action)
    {
        if(!handlerDict.ContainsKey(type))
        {
            handlerDict.Add(type, action);
        }
        else
        {
            handlerDict[type] += action;
        }
    }

    /// <summary>
    /// 서버가 보낸 데이터를 파싱하고 dictionary 에 type 을 넘겨 줌
    /// </summary>
    /// <param name="data">DataVO based JSON string from server.</param>
    public void Handle(string data)
    {
        DataVO vo;

        try
        {
            vo = JsonUtility.FromJson<DataVO>(data);
        }
        catch
        {
            Debug.LogError($"서버로부터 넘어온 메세지: {data} 는 DataVO 형식이 아닙니다.");
            return;
        }

        if(!handlerDict.ContainsKey(vo.type))
        {
            Debug.LogError($"BufferHandler: \"{vo.type}\"\r\n그런 타입이 없습니다.");
            return;
        }

        lock(lockObj)
        {
            receivedVOQueue.Enqueue(vo);
        }
    }

    private void Update()
    {
        if(receivedVOQueue.Count != 0)
        {
            DataVO vo;
            lock(lockObj)
            {
                vo = receivedVOQueue.Dequeue();
            }

            // Dictionary 에 추가할 때 값도 필수로 추가하게 되서
            handlerDict[vo.type](vo.payload);
        }
    }
}
