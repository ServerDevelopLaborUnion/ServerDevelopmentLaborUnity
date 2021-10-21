using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BufferHandler : MonoSingleton<BufferHandler>
{
    private Queue<DataVO> msgQueue = new Queue<DataVO>(); // 유니티 스레드에서 돌리기 위함
    private object lockObj = new object(); // lock();
    private Dictionary<string, System.Action<string>> handlerDict = new Dictionary<string, System.Action<string>>();


    /// <summary>
    /// 서버에서 메세지를 보냈을 때 type 에 따라 실행될 Action
    /// </summary>
    /// <param name="type">이벤트 타입</param>
    /// <param name="action">타입에 따른 Action, DataVO.payload 가 메개변수로 전달됨</param>
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
    /// 서버에서 메세지를 보냈을 때 type 에 따라 실행될 Action 을 삭제함
    /// </summary>
    /// <param name="type">이벤트 타입</param>
    /// <param name="action">삭제할 Action</param>
    public void RemoveHandler(string type, System.Action<string> action)
    {
        if(!handlerDict.ContainsKey(type))
        {
            Debug.LogError($"type:{type}, 그런 타입이 없습니다.");
            return;
        }
        handlerDict[type] -= action;
    }

    public void Handle(string data)
    {
        DataVO vo = null;

        try
        {
            vo = JsonUtility.FromJson<DataVO>(data);
            if(vo == null) throw new System.Exception("서버에서 보낸 메세지를 변환할 수 없습니다.");
        }
        catch(System.Exception e)
        {
            Debug.LogError($"서버로부터 메세지를 받는 도중 오류가 발생했습니다.\r\n{e.Message}\r\n{e.StackTrace}");
            return;
        }

        if(!handlerDict.ContainsKey(vo.type))
        {
            Debug.LogError($"BufferHandler > {vo.type}. 그런 타입이 없습니다.");
            return;
        }

        lock(lockObj)
        {
            msgQueue.Enqueue(vo);
        }
    }

    private void Update()
    {
        if(msgQueue.Count > 0)
        {
            DataVO vo;
            lock(lockObj)
            {
                vo = msgQueue.Dequeue();
            }

            Debug.Log(vo.type);
            Debug.Log(vo.payload);
            
            handlerDict[vo.type](vo.payload);
        }
    }


}
