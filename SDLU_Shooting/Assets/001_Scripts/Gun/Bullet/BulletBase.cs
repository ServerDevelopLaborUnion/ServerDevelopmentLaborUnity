using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 모든 총알이 상속받을 클레스
// 별 특별한 총알이 아니면 이거 그냥 써도 됩니다.

public class BulletBase : MonoBehaviour
{
    public int bulletDamage; // 총 데미지. TODO : 나중에 설정을 받아 와 줘야 함

    private new BoxCollider collider; // 충돌 체크 용, hiding 문제가 있어서 앞에다 new 를 써 줬어요.

    protected virtual void Awake()
    {
        collider = GetComponent<BoxCollider>();
    }

    protected virtual void OnCollisionEnter(Collision other)
    {
        Rigidbody hitRigid = other.gameObject.GetComponent<Rigidbody>(); // 반작용 용도

        other.gameObject.GetComponent<IDamageable>()?.OnDamage(bulletDamage); // 데미지 처리. TODO : 속도와 거리에 따라 다른 데미지?

        if(hitRigid != null)
        {
            ReactionManager.Reaction(hitRigid, 1.0f); // TODO : 방향 따라 밀어야 함, 미는 힘도 조정해야 함
        }

        Disable();
    }


    protected virtual void Disable()
    {
        gameObject.SetActive(false);
    }
}
