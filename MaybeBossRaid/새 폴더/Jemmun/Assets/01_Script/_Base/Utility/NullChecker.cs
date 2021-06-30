using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class NullChecker : MonoBehaviour
{

    /// <summary>
    /// null üũ�� ���ִ� �Լ�
    /// </summary>
    /// <param name="obj">äũ�� ����</param>
    /// <param name="stopUnity">null �϶� ����Ƽ�� ���� �� (�⺻���δ� false �� �� �־��)</param>
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
