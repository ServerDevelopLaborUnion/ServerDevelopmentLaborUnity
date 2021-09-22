using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 카메라 시아 관련

public class CameraFOVManager : MonoBehaviour
{
    [SerializeField] private Camera render; // 화면 렌더링하는 카메라
    [SerializeField] private Camera gun; // 총 따로 렌더링하는 카메라

#region Default FOV
    private float gunAimFoV     = 16.5f; // 총 카메라 조준 FoV
    private float gunFoV = 45.0f; // 총 카메라 일반 FoV
    private float camFov       = 85.0f; // 일반 FoV
    private float camAimFoV;                   // 조준 FoV
#endregion

    private void Awake()
    {
        camFov       = render.fieldOfView;
        camAimFoV    = camFov / 1.2f; // TODO : 뒤에 들어가는 숫자는 배율
        gunFoV       = gun.fieldOfView;
    }

    private void Start()
    {
        Shootable.OnAim += (t) => {
            render.fieldOfView = Mathf.Lerp(camFov, camAimFoV, t);
            gun.fieldOfView    = Mathf.Lerp(gunFoV, gunAimFoV, t);
        };
    }
}
