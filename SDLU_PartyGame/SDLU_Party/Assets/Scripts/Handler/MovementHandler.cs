using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    void Start()
    {
        BufferHandler.Instance.AddHandler("move", (data) => {
            MoveVO vo = JsonUtility.FromJson<MoveVO>(data);
            UserManager.Instance.SetTransform(vo.id, vo.position, vo.rotation);
        });
    }
}
