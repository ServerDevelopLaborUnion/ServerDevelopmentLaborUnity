using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 연사가 가능한 총의 부모 클레스 입니다.
// 이걸 그냥 넣으면 안대요...

public class FullAutoGuns : Shootable
{
    public bool Fullauto { get; protected set; } // 연사
    public bool Burst { get; protected set; }    // 점사
    public bool Semi { get; protected set; }     // 단발

    private float lastFireTime = 0; // 총 발사 속도 용도

    [SerializeField] private float pushbackDecrease = 80.0f; // 작용 반작용 효과 용
    

    [SerializeField] private CharactorInput input = null;

    // TODO : 엄청난 SRP 위반
    // 추후에 나누어 두겟스빈다.
    protected override void Shoot() // 총 발사
    {
        if(reloading || ammo <= 0 || Time.time < lastFireTime + fireInterval) return; // 총알이 없거나 아직 발사 텀이 안 됬을 때
        --ammo;

        lastFireTime = Time.time; // 마지막 발사 시간을 현제 시간으로

        // 총알을 가져옴
        GameObject bullet = BulletPool.Get();

        // 총알을 발사함
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * launchForce, ForceMode.Impulse);

        // reaction
        ReactionManager.Reaction(GameManager.instance.playerRigid, launchForce, pushbackDecrease);

        // 반동
        float xRecoil = Random.Range(minRecoil, maxRecoil);
        float yRecoil = Random.Range(-minRecoil, minRecoil);

        GameManager.instance.playerRigid.rotation *= Quaternion.Euler(GameManager.instance.playerRigid.rotation.x - xRecoil, GameManager.instance.playerRigid.rotation.y + yRecoil, GameManager.instance.playerRigid.rotation.z);
    }

    protected override void Reload()
    {
        base.Reload();
    }

    private void Update()
    {
        if(input.Shoot)
        {
            Shoot();
        }
        
        if(input.Reload)
        {
            Reload();
        }
    }
}
