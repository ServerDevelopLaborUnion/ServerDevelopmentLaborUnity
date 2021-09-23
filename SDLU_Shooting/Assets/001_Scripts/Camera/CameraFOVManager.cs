using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 카메라 시아 관련

public class CameraFOVManager : MonoBehaviour
{
    static public CameraFOVManager instance = null;

    [SerializeField] private Camera render; // 화면 렌더링하는 카메라
    [SerializeField] private Camera gun;    // 총 따로 렌더링하는 카메라

#region FOV
    private float gunAimFoV = 16.5f;     // 총 카메라 조준 FoV
    private float gunFoV    = 45.0f;     // 총 카메라 일반 FoV
    private float camAimFoV;             // 조준 FoV
    private float aimMultiplier = 1.25f; // 조준 배율
    public  float camFov    = 85.0f;     // 일반 FoV
#endregion


    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("There is more than one CameraFOFManager running at same scene");
        }
        instance = this;

        camFov       = render.fieldOfView;
        camAimFoV    = camFov / aimMultiplier; // 배율을 나누어줍니다.
        gunFoV       = gun.fieldOfView;
    }

    private void Start()
    {
        Shootable.OnAim += (t) => {
            render.fieldOfView = Mathf.Lerp(camFov, camAimFoV, t);
            gun.fieldOfView    = Mathf.Lerp(gunFoV, gunAimFoV, t);
        };
    }

    public void SetMultiplier(float multiplier)
    {
        if(multiplier < 1.0f)
        {
            Debug.LogWarning($"multiplier should be equal or bigger than 1.\r\nInput: {multiplier}");
            return;
        }
        
        aimMultiplier = multiplier;
        camAimFoV     = camFov / aimMultiplier;
    }
}
