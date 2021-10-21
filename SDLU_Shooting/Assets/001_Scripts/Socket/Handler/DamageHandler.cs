using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler: MonoBehaviour
{
    private void Start()
    {
        BufferHandler.Instance.AddHandler("damage", (data) =>
        {
            DamageVO vo = JsonUtility.FromJson<DamageVO>(data);

            GameManager.instance.playerBase.OtherCharactorDamage(vo.id, vo.damage);
        });
    }
}