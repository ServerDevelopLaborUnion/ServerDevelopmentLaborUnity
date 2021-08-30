using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicControl : MonoBehaviour
{
    static private BasicControl inst = null; // static 함수 용

    private Dictionary<KeyCode, Action> charactorControlDict = new Dictionary<KeyCode, Action>(); // 캐릭터 입력에 관한 함수

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


    static public void Play(KeyCode key)
    {
        inst.charactorControlDict[key]();
    }
}
