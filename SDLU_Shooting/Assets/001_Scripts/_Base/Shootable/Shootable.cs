using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 모든 총기류가 상속받는 클레스

abstract public class Shootable : MonoBehaviour
{
    [Header("발사체 (또는 총알)")]
    [SerializeField] protected GameObject projectile = null; 

    [Header("발사 위치")]
    [SerializeField] protected Transform fireTrm = null;

    [Header("총알 발사 힘")]
    [SerializeField] protected float launchForce = 20.0f;

    [Header("탄창 용량")]
    [SerializeField] protected int maxAmmo = 0;
                     protected int ammo = 0;

    [Header("발사 딜레이")]
    [SerializeField] protected float fireInterval = 0.25f;

    [Header("재장전 시간")]
    [SerializeField] protected float reloadDuration = 2.0f;


    protected bool reloading = false; // 재장전 중인지

    private void Awake()
    {
        ammo = maxAmmo;
    }

    abstract protected void Shoot(); // 총 발사

    protected virtual void Reload() // 재장전 시작
    {
        reloading = true;

        Invoke(nameof(OnReloadFinish), reloadDuration);
    }
    protected virtual void OnReloadFinish() // 재장전 끝
    {
        reloading = false;
        ammo = maxAmmo;
    }


}
