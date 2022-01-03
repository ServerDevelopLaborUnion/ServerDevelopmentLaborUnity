using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public List<Slider> _cooltimeText = new List<Slider>();

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("다수의 UIManager가 돌아가고 있습니다. ");
        }
        instance = this;
    }
}
