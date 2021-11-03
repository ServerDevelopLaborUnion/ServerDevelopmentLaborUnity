using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootHandler : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float firePower;


    #region 이벤트
    private void Start(){
        BufferHandler.Instance.AddHandler("shoot", (data) =>
        {
            ShootVO vo = JsonUtility.FromJson<ShootVO>(data);

            if(vo.id == GameManager.Instance.Player.ID){
                Bullet bullet = BulletPool.Instance.Get();
                bullet.transform.position = vo.position;
                bullet.transform.rotation = Quaternion.Euler(vo.rotation);
                bullet.Fire(bullet.transform.forward * firePower, () =>
                {
                    Debug.Log("잉");
                });
            }
        });
    }
    #endregion

}
