using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootHandler : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    #region 메소드
    private void Start(){
        BufferHandler.Instance.AddHandler("shoot", (data) =>
        {
            ShootVO vo = JsonUtility.FromJson<ShootVO>(data);

            if(vo.id == GameManager.instance.playerBase.ID)
                Instantiate(bulletPrefab, vo.position, Quaternion.Euler(vo.rotation));
        });
    }
    #endregion
}
