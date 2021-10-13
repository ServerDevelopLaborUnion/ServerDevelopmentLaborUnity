using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    static private T _instance = null;

    static public T Instance
    {
        get
        {
            if(_instance == null)
            {
                T[] obj = FindObjectsOfType<T>();

                if(obj.Length > 0)
                {
                    _instance = obj[0];
                }
                if(obj.Length > 1)
                {
                    Debug.LogError($"There are more than one {_instance.GetType()} running at same scene.");
                }
            }
            return _instance;
        }
    }
}
