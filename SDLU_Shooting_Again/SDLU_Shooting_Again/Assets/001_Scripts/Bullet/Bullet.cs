using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class Bullet : MonoBehaviour
{
    event System.Action _onCollision; // 물체와 충돌 시 호출
    Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>(); // RequireComponent 으로 null 발생 불가능
    }

    /// <summary>
    /// 총알을 발사합니다.
    /// </summary>
    /// <param name="velocity">발사 힘</param>
    /// <param name="onCollision">물체와 충돌 시 실행될 Callback</param>
    public void Fire(Vector3 velocity, System.Action onCollision)
    {
        rigid.velocity = Vector3.zero;
        
        gameObject.SetActive(true);

        rigid.AddForce(velocity, ForceMode.Impulse);

        _onCollision = onCollision;
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name);
        other.gameObject.GetComponent<Player>().Damaged(0);
        //TODO : 0바꾸기
        SocketClient.Instance.Send(new DataVO("damage", JsonUtility.ToJson(new DamageVO(1, 10))));
        //TODO : 1, 10 바꾸기
        _onCollision();
    }
}
