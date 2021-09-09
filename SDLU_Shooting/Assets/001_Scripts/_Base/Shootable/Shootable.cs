using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 모든 총기류가 상속받는 클레스
// 추후 나누어 두겠습니다.

abstract public class Shootable : MonoBehaviour
{
    [SerializeField] protected CharactorInput input     = null; // 입력
    [SerializeField] protected Camera         gunCamrea = null; // 총 렌더링 용 카메라

    [Header("발사체")]
    [SerializeField] protected GameObject projectile = null; // 발사체
    [SerializeField] protected Transform  fireTrm    = null; // 발사될 위치

    [Header("발사")]
    [SerializeField] protected float launchForce  = 20.0f; // 발사 힘
    [SerializeField] protected int    maxAmmo     = 0;     // 최대 탄약 수
                     protected int    ammo        = 0;     // 현재 남은 탄약 수
    [SerializeField] protected float fireInterval = 0.25f; // 발사 간격
                     protected float lastFireTime = 0;     // 마지막 발사 시간

    [Header("반동")]
    [SerializeField] protected float maxRecoil        = 1.0f;  // 최대 반동
    [SerializeField] protected float minRecoil        = 0.8f;  // 최소 반동
    [SerializeField] protected float pushbackDecrease = 80.0f; // 반작용 감소

    [Header("재장전")]
    [SerializeField] protected float reloadDuration = 2.0f;                 // 재장전 기간
                     protected bool  reloading      = false;                // 재장전 중인지
                     public    bool  MagEmpty       { get; protected set; } // 탄약 잔여 여부

    [Header("조준")]
    [SerializeField] protected float     aimMultiplier       = 1.25f;        // 조준 배율
    [SerializeField] protected float     aimingDuration      = 1.0f;         // 조준 시간
    [SerializeField] private   Transform aimPos              = null;         // 조준 포지션
                     private   Vector3   aimVector           = Vector3.zero; // 조준 위치
    [SerializeField] private   Transform defaultPos          = null;         // 일반 포지션
                     private   Vector3   idleVector          = Vector3.zero; // 일반 위치
    [SerializeField] private   Transform GunPosition         = null;         // 현재 총의 위치
    [SerializeField] private   float     gunCameraAimFoV     = 16.5f;        // 총 카메라 조준 FoV
    [SerializeField] private   float     gunCameraDefaultFoV = 45.0f;        // 총 카메라 일반 FoV
                     private   float     camDefaultFoV       = 85.0f;        // 일반 FoV
                     private   float     camAimFoV;                          // 조준 FoV


    protected virtual void Awake()
    {
        ammo      = maxAmmo;
        camAimFoV = camDefaultFoV / aimMultiplier; // 매번 연산하기 싫어서 흠흠

        aimVector = aimPos.localPosition;
        idleVector = defaultPos.localPosition;

        StartCoroutine(Aim());
    }

    protected virtual void Shoot() // 총 발사
    {
        if(MagEmpty) return;

        MagEmpty = ammo <= 0;
        --ammo;

        // 총알을 가져옴
        GameObject bullet = BulletPool.Get();

        // 총알을 발사함
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * launchForce, ForceMode.Impulse);
    } 

    protected virtual void Recoil() // 반동
    {
        float xRecoil = Random.Range( minRecoil, maxRecoil);
        float yRecoil = Random.Range(-minRecoil, minRecoil);

        GameManager.instance.playerRigid.rotation *=
                        Quaternion.Euler(GameManager.instance.playerRigid.rotation.x - xRecoil,
                                         GameManager.instance.playerRigid.rotation.y + yRecoil,
                                         GameManager.instance.playerRigid.rotation.z);
    }

    ///<returns>true when unable</returns>
    protected virtual bool Fireable()
    {
        return reloading || MagEmpty
                         || (Time.time < lastFireTime + fireInterval); // 재장전 중이거나, 총알이 없거나, 아직 발사 시간이 안 됬거나
    }

    protected virtual void Reload() // 재장전 시작
    {
        if(reloading) return;
        
        reloading = true;
    
        Invoke(nameof(OnReloadFinish), reloadDuration);
    }

    protected virtual IEnumerator Aim() // 조준
    {
        float t = 0.0f;

        while(true)
        {
            t += input.Aim ? (Time.deltaTime / aimingDuration) : -(Time.deltaTime / aimingDuration);
            t  = Mathf.Clamp(t, 0.0f, 1.0f); // 범위 밖에 안 나가게


            Camera.main.fieldOfView   = Mathf.Lerp(camDefaultFoV, camAimFoV, t);
            GunPosition.localPosition = Vector3.Lerp(idleVector, aimVector, t);
            gunCamrea.fieldOfView     = Mathf.Lerp(gunCameraDefaultFoV, gunCameraAimFoV, t);
            

            yield return null;
        }
    }

    protected virtual void OnReloadFinish() // 재장전 끝
    {
        reloading = false;
        ammo      = maxAmmo;
        MagEmpty  = false;
    }


}
