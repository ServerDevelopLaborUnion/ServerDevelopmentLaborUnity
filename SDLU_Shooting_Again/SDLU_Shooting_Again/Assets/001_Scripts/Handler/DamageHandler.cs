using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    private void Start()
    {
        BufferHandler.Instance.AddHandler("damage", (data) =>
        {
            DamageVO vo = JsonUtility.FromJson<DamageVO>(data);
            
            if (GameManager.Instance.Player.ID == vo.id)
            {
                return;
            }

            UserManager.Instance.Get(vo.id).Damaged(vo.damage);
        });
    }
}
