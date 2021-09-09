using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 모든 총알이 상속받을 클레스
// 별 특별한 총알이 아니면 이거 그냥 써도 됩니다.

public class BulletBase : MonoBehaviour
{
    public int bulletDamage; // 총 데미지. TODO : 나중에 설정을 받아 와 줘야 함
    private new BoxCollider collider; // 충돌 체크 용, hiding 문제가 있어서 앞에다 new 를 써 줬어요.
    private Rigidbody rigid;          // 비활성화 시 velocity 초기화 용도

    private Vector3 zeroVector = Vector3.zero; // static 으로 계속 접근하고 싶지 않앗스빈다. 흠흠

    private WaitForSeconds wait = new WaitForSeconds(20.0f); // 일정 시간 후 사라지기 위해서

    protected virtual void Awake()
    {
        collider = GetComponent<BoxCollider>();
        rigid    = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        StartCoroutine(AutoDisable());
    }

    protected virtual void OnCollisionEnter(Collision other)
    {
        Rigidbody hitRigid = other.gameObject.GetComponent<Rigidbody>(); // 반작용 용도

        other.gameObject.GetComponent<IDamageable>()?.OnDamage(bulletDamage); // 데미지 처리. TODO : 속도와 거리에 따라 다른 데미지?

        if(hitRigid != null)
        {
            ReactionManager.Reaction(hitRigid, 1.0f); // TODO : 방향 따라 밀어야 함, 미는 힘도 조정해야 함
        }

        StopCoroutine(AutoDisable());
        Disable();
    }

    protected virtual IEnumerator AutoDisable()
    {
        yield return wait;
        Disable();
    }

    protected virtual void Disable() // 비활성화
    {
        rigid.velocity = zeroVector; // 계속 날라가면 조금 그러니
        gameObject.SetActive(false);
    }
}
