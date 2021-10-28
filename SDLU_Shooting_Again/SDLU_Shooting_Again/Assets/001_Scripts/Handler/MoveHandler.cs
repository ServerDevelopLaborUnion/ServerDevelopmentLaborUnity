using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHandler : MonoBehaviour
{
    private void Start()
    {
        BufferHandler.Instance.AddHandler("move",(msg) =>
        {
            MoveVO vo = JsonUtility.FromJson<MoveVO>(msg);
            if (GameManager.Instance.Player.ID == vo.id)
            {
                return;
            }

            UserManager.Instance.Get(vo.id).gameObject.transform.position = vo.pos;
            UserManager.Instance.Get(vo.id).gameObject.transform.eulerAngles = vo.rot;

        });
    }
}
