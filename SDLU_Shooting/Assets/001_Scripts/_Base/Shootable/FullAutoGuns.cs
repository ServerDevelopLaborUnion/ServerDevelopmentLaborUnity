using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 연사가 가능한 총의 부모 클레스 입니다.
// 그대로 사용해도 되고 상속을 받아서 사용해도 됩니다.

public class FullAutoGuns : Shootable
{
    public bool Fullauto { get; protected set; } // 연사 여부. false = 단발

    protected bool triggerResetted = true;

    protected override void Awake()
    {
        base.Awake();

        Fullauto = true;
    }

    protected virtual void Update() // TODO : 구독발행?
    {
        if(input.Shoot)
        {
            switch(Fullauto)
            {
                case true:
                    Shoot();
                    break;

                case false:
                    if(triggerResetted)
                        Shoot();
                    break;

            }
            triggerResetted = false;
        }
        else
        {
            triggerResetted = true;
        }
        
        if(input.Reload)
        {
            Reload();
        }

        if(input.FireMode)
        {
            Fullauto = !Fullauto;
        }
    }

    protected override void Shoot() // 총 발사
    {
        if (!Fireable()) { return; } // 총알이 없거나 아직 발사 텀이 안 됬을 때

        lastFireTime = Time.time; // 마지막 발사 시간을 현제 시간으로

        base.Shoot(); // 발사
        Recoil();

        // 반작용
        ReactionManager.Reaction(GameManager.instance.playerRigid, launchForce, pushbackDecrease);
    }

    protected override void Reload() // 재장전
    {
        base.Reload();
    }

}
