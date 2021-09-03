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

    [Header("총알 발사 힘과 반작용")]
    [SerializeField] protected float launchForce      = 20.0f;
    [SerializeField] protected float pushbackDecrease = 80.0f;
    
    [Header("반동")]
    [SerializeField] protected float maxRecoil = 1.0f;
    [SerializeField] protected float minRecoil = 0.8f;

    [Header("탄창 용량")]
    [SerializeField] protected int maxAmmo = 0;
                     protected int ammo = 0;

    [Header("발사 딜레이")]
    [SerializeField] protected float fireInterval = 0.25f;
                     protected float lastFireTime = 0; // 마지막 발사 시간

    [Header("재장전 시간")]
    [SerializeField] protected float reloadDuration = 2.0f;

    protected bool reloading = false; // 재장전 중인지

    public bool MagEmpty { get; protected set; } // 탄약 잔여 여부

    private void Awake()
    {
        ammo = maxAmmo;
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
        return reloading || MagEmpty || (Time.time < lastFireTime + fireInterval); // 재장전 중이거나, 총알이 없거나, 아직 발사 시간이 안 됬거나
    }


    protected virtual void Reload() // 재장전 시작
    {
        if(reloading) return;
        
        reloading = true;
    
        Invoke(nameof(OnReloadFinish), reloadDuration);
    }
    protected virtual void OnReloadFinish() // 재장전 끝
    {
        reloading = false;
        ammo = maxAmmo;
        MagEmpty = false;
    }


}
