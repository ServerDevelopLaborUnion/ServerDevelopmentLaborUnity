using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 모든 총기류가 상속받는 클레스
// 추후 나누어 두겠습니다.

abstract public class Shootable : MonoBehaviour
{
    [SerializeField] protected CharactorInput input  = null; // 입력
    [SerializeField] protected BulletPool bulletPool = null; // 풀링

    [Header("발사")]
    [SerializeField] protected float launchForce  = 100.0f; // 발사 힘
    [SerializeField] protected int   damage       = 20;     // 데미지
    [SerializeField] protected float fireInterval = 0.25f;  // 발사 간격
                     protected float lastFireTime = 0;      // 마지막 발사 시간
    [SerializeField] protected int   maxAmmo      = 0;      // 최대 탄약 수
    public int Ammo { get; protected set; }                 // 현재 남은 탄약 수


    [Header("반동")]
    [SerializeField] protected float maxRecoil        = 1.0f;   // 최대 반동
    [SerializeField] protected float minRecoil        = 0.8f;   // 최소 반동
    [SerializeField] protected float pushbackDecrease = 300.0f; // 반작용 감소


    [Header("재장전")]
    [SerializeField] protected float reloadDuration = 2.0f;  // 재장전 기간
    public bool Reloading { get; protected set; }            // 재장전 중인지


    [Header("조준")]
    [SerializeField] protected float aimingDuration = 1.0f; // 조준 소요 시간


    static public event System.Action<float> OnAim; // 조준 상태에 따라 t 가 전달될 것
    static public event System.Action OnReloaded; // 재장전 끝났다면 호출
    static public event System.Action OnFire; // 발사 시 호출 TODO : 초탄 여부를 bool 로 념겨줘야하나

    protected virtual void Awake()
    {
        Ammo = maxAmmo;

        // 빈 Action 을 호출하게 되면 예외가 발생함
        OnAim      = (t) => { };
        OnReloaded = () => { };
        OnFire     = () => { };

        StartCoroutine(Aim());
    }

#region Fire
    protected virtual void Shoot() // 총 발사
    {
        if(Ammo <= 0) return;

        --Ammo;

        // 총알을 가져옴
        GameObject bullet = bulletPool.Get();

        // 총알을 발사함
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * launchForce, ForceMode.Impulse);
        bullet.GetComponent<BulletBase>().Damage = damage;

        OnFire(); // TODO : 반동도 여기서?
    }

    ///<returns>true when able</returns>
    protected virtual bool Fireable()
    {
        return !(Reloading || Ammo <= 0
                           || (Time.time < lastFireTime + fireInterval)); // 재장전 중이거나, 총알이 없거나, 아직 발사 시간이 안 됬거나
    }
#endregion

    protected virtual void Recoil() // 반동
    {
        float xRecoil = Random.Range( minRecoil, maxRecoil);
        float yRecoil = Random.Range(-minRecoil, minRecoil);

        GameManager.instance.playerRigid.rotation *=
                        Quaternion.Euler(GameManager.instance.playerRigid.rotation.x - xRecoil,
                                         GameManager.instance.playerRigid.rotation.y + yRecoil,
                                         GameManager.instance.playerRigid.rotation.z);
    }

    protected virtual IEnumerator Aim() // 조준
    {
        float t = 0.0f;

        while(true)
        {
            t += input.Aim ? (Time.deltaTime / aimingDuration) : -(Time.deltaTime / aimingDuration);
            t  = Mathf.Clamp(t, 0.0f, 1.0f); // 범위 밖에 안 나가게

            OnAim(t);

            yield return null;
        }
    }

#region Reload
    protected virtual void Reload() // 재장전 시작
    {
        if (Reloading) return;

        Reloading = true;

        Invoke(nameof(OnReloadFinish), reloadDuration);
    }

    protected virtual void OnReloadFinish() // 재장전 끝
    {
        Reloading = false;
        Ammo = maxAmmo;

        OnReloaded();
    }
#endregion

}
