using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class NullChecker : MonoBehaviour
{

    /// <summary>
    /// null 체크를 해주는 함수, null 인 경우 에디터를 종료시키니 조심하세요.
    /// </summary>
    /// <param name="obj">채크할 무언가</param>
    static public void CheckNULL(object obj)
    {
#if UNITY_EDITOR
        if (obj == null)
        {
            Debug.LogError($"{obj} is null. Quitting");
            EditorApplication.isPlaying = false;
        }
#endif
    }
}
