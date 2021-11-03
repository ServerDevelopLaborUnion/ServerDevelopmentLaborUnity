using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WasabeTestManager : MonoBehaviour
{
    [SerializeField] private Transform firePos;
    [SerializeField] Shootable shootable;

private void Start() {
        shootable.OnFire += () => {
            SocketClient.Instance.Send(new DataVO("shoot", JsonUtility.ToJson(new ShootVO(GameManager.Instance.Player.ID , firePos.position,GameManager.Instance.Player.transform.eulerAngles))));
        };
}

    private void Update() {
        // if(Input.GetKeyDown(KeyCode.Space)){
        // }
        // if(Input.GetButtonDown("Jump")){
        //     SocketClient.Instance.Send(new DataVO("dead", JsonUtility.ToJson(new DeadVO(GameManager.Instance.Player.ID))));
        // }
    }
}
