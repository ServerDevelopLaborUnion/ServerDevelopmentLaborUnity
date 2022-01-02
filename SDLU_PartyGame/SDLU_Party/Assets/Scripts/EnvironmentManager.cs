using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class EnvironmentManager : MonoBehaviour
{
    private Volume _vol;
    private Vignette _vignette;
    private ColorAdjustments _colorAdjustments;
    public static EnvironmentManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("�ټ��� EnvironManager�� ���ư��� �ֽ��ϴ�. ");
        }
        instance = this;
        _vol = GetComponent<Volume>();
        _vol.profile.TryGet<Vignette>(out _vignette);
        _vol.profile.TryGet<ColorAdjustments>(out _colorAdjustments);
    }

    public void DeadEnvironment()
    {
        _vignette.active = false;
        _colorAdjustments.active = true;
    }

    public void AliveEnvironment()
    {
        _vignette.active = true;
        _colorAdjustments.active = false;
    }
}
