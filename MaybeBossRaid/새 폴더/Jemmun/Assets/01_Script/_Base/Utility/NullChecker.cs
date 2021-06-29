using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class NullChecker : MonoBehaviour
{

    /// <summary>
    /// null üũ�� ���ִ� �Լ�, null �� ��� �����͸� �����Ű�� �����ϼ���.
    /// </summary>
    /// <param name="obj">äũ�� ����</param>
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
