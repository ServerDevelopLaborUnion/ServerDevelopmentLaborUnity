using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHandler : MonoBehaviour
{
    [SerializeField] float lerpAmount = 0.7f;

    private void Start()
    {
        BufferHandler.Instance.AddHandler("move",(msg) =>
        {
            MoveVO vo = JsonUtility.FromJson<MoveVO>(msg);
            if (GameManager.Instance.Player.ID == vo.id)
            {
                return;
            }

            Transform targetTrm = UserManager.Instance.Get(vo.id).gameObject.transform;
            if(targetTrm == null) return;

            Vector3 newPos  = JsonUtility.FromJson<Vector3>(vo.pos);
            Vector3 lastPos = targetTrm.position;

            Vector3 newRot  = JsonUtility.FromJson<Vector3>(vo.rot);
            Vector3 lastRot = targetTrm.eulerAngles;

            targetTrm.position    = Vector3.Lerp(lastPos, newPos, lerpAmount);
            targetTrm.eulerAngles = Vector3.Lerp(lastRot, newRot, lerpAmount);


        });
    }
}
