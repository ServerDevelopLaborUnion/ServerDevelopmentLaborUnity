using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 모든 인풋에 대한 처리를 저장하는 클레스

public class BasicControl : MonoBehaviour
{
    static private BasicControl inst = null; // static 함수 용

    private Dictionary<KeyCode, Action> charactorControlDict = new Dictionary<KeyCode, Action>(); // 캐릭터 입력에 관한 함수
    private Dictionary<MouseButton, Action> charactorMouseDict = new Dictionary<MouseButton, Action>(); // 캐릭터 마우스 입력에 관한 함수...

    private void Awake()
    {
        inst = this;
    }

    ///<summary>
    ///캐릭터 이동 코드
    ///</summary>
    static public void AddCharactorAction(KeyCode key, Action action)
    {
        if(inst.charactorControlDict.ContainsKey(key))
        {
            Debug.LogError($"BasicControl: Key {key} already exisits.\r\n");
            return;
        }

        inst.charactorControlDict.Add(key, action);
    }
    static public void AddCharactorAction(MouseButton key, Action action)
    {
        if(inst.charactorMouseDict.ContainsKey(key))
        {
            Debug.LogError($"BasicControl: Key {key} already exisits.\r\n");
            return;
        }

        inst.charactorMouseDict.Add(key, action);
    }


    static public void Play(KeyCode key)
    {
        inst.charactorControlDict[key]();
    }
    static public void Play(MouseButton key)
    {
        inst.charactorMouseDict[key]();
    }
}
