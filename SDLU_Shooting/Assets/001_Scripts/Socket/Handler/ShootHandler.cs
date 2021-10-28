using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootHandler : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    #region 이벤트
    private void Start(){
        BufferHandler.Instance.AddHandler("shoot", (data) =>
        {
            ShootVO vo = JsonUtility.FromJson<ShootVO>(data);

            if(vo.id == GameManager.instance.playerBase.ID){
                GameObject bullet = GameManager.instance.bulletPool.Get();
                bullet.transform.position = vo.position;
                bullet.transform.rotation = Quaternion.Euler(vo.rotation);
            }
        });
    }
    #endregion
}
