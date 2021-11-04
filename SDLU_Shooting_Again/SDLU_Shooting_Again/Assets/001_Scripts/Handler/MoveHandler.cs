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

            CharactorBase targetBase = UserManager.Instance.Get(vo.id);
            if(targetBase == null) return;
            
            RemotePlayer target = targetBase.gameObject.transform.GetComponent<RemotePlayer>();
            if(target == null) return;

            Vector3 newPos  = JsonUtility.FromJson<Vector3>(vo.pos);
            Vector3 newRot  = JsonUtility.FromJson<Vector3>(vo.rot);

            target.SetTargetTransform(newPos, newRot);

        });
    }
}
