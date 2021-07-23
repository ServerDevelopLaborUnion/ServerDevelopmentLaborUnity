using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Debug.Log($"{gameObject.name} is now in DontDestroyOnLoad.");
    }
}
