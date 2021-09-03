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



    [SerializeField] private CharactorInput input = null;

    // TODO : 엄청난 SRP 위반
    // 추후에 나누어 두겟스빈다.
    protected override void Shoot() // 총 발사
    {
        if(Fireable()) { return; } // 총알이 없거나 아직 발사 텀이 안 됬을 때

        lastFireTime = Time.time; // 마지막 발사 시간을 현제 시간으로

        base.Shoot(); // 발사
        Recoil();

        // 반작용
        ReactionManager.Reaction(GameManager.instance.playerRigid, launchForce, pushbackDecrease);
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
