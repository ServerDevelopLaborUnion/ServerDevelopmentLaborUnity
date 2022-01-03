using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public List<Slider> _cooltimeSlider = new List<Slider>();
    public List<Image> _cooltimeImage = new List<Image>();
        
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("다수의 UIManager가 돌아가고 있습니다. ");
        }
        instance = this;

        
    }
}
