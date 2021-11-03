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

            Debug.LogWarning(vo.id);
            Debug.LogWarning(vo.pos);
            Debug.LogWarning(vo.rot);

            UserManager.Instance.Get(vo.id).gameObject.transform.position = JsonUtility.FromJson<Vector3>(vo.pos);
            UserManager.Instance.Get(vo.id).gameObject.transform.eulerAngles = JsonUtility.FromJson<Vector3>(vo.rot);

        });
    }
}
