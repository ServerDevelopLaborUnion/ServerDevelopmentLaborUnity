using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAimManager : MonoBehaviour
{
    [SerializeField] Transform idleGunTrm; // 총 일반 위치
    [SerializeField] Transform aimGunTrm; // 총 조준 위치
    [SerializeField] Transform firePosition; // 발사 위치
    [SerializeField] Transform scopePosition; // 스코프 위치


    [SerializeField] Camera mainCam;
    [SerializeField] Camera gunCam;


    [Header("조준 시 시아")]
    [SerializeField] float aimFoVMainCam;
    [SerializeField] float aimFoVGunCam;
                     float idleFoVMainCam;
                     float idleFoVGunCam;

    [SerializeField] float aimDelay = 0.8f;

    float ep = 0.0f; // 무질서도
    bool isAim = false;

    Vector3 idlePos;
    Vector3 aimPos;

    Vector3 idleFirePos;
    Vector3 aimFirePos;


    private void Start()
    {
        idleFirePos = firePosition.localPosition;
        idlePos = idleGunTrm.localPosition;
        aimPos  = aimGunTrm.localPosition;

        idleFoVMainCam = mainCam.fieldOfView;
        idleFoVGunCam  = gunCam.fieldOfView;

        InputManager.Instance.OnMouseAim += () => {
            isAim = true;
        };

        InputManager.Instance.OnMouseAimQuit += () => {
            isAim = false;
        };
    }


    private void Update()
    {
        // 무질서도 연산
        ep += isAim ? Time.deltaTime / aimDelay : -Time.deltaTime / aimDelay;
        ep = Mathf.Clamp(ep, 0.0f, 1.0f);

        // 조준
        transform.localPosition    = Vector3.Lerp(idlePos, aimPos, ep);             // 총 위치
        firePosition.localPosition = Vector3.Lerp(idleFirePos, scopePosition.localPosition, ep);     // 발사 위치
        mainCam.fieldOfView        = Mathf.Lerp(idleFoVMainCam, aimFoVMainCam, ep); // 메인 카메라 FoV
        gunCam.fieldOfView         = Mathf.Lerp(idleFoVGunCam, aimFoVGunCam, ep);   // 총 렌더링 카메라 FoV
    }
}
