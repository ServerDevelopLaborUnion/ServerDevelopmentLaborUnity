using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler: MonoBehaviour
{
    private void Start()
    {
        BufferHandler.Instance.AddHandler("damage", (data) =>
        {
            DataVO vo = JsonUtility.FromJson<DataVO>(data);

            //GameManager.instance.playerBase.OtherCharactorDamage(vo.id, vo.payload);
        });
    }
}