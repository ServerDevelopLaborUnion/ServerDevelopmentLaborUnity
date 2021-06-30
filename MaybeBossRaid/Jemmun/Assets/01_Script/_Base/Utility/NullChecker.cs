using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class NullChecker : MonoBehaviour
{

    /// <summary>
    /// null 체크를 해주는 함수
    /// </summary>
    /// <param name="obj">채크할 무언가</param>
    /// <param name="stopUnity">null 일때 유니티를 멈출 지 (기본으로는 false 가 들어가 있어요)</param>
    static public void CheckNULL(object obj, bool stopUnity = false)
    {
#if UNITY_EDITOR
        if (obj == null)
        {
            Debug.LogError($"{obj} is null.");

            if(stopUnity)
            {
                Debug.LogError("object is null, quitting");
                EditorApplication.isPlaying = false;
            }
        }
#endif
    }
}
