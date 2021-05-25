using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LoginHandler : MonoBehaviour, IMsgHandler
{
    public Text nameInput;
    
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void HandleMsg(string payload)
    {
        TransformVO vo = JsonUtility.FromJson<TransformVO>(payload);
        
        GameManager.instance.ChangeToGame(vo.point, vo.rotation, vo.socketId); //게임매니저를 호출해서 넘긴다.
    }

    public void SendMsg(){
        //여기서 nameInput에 입력값이 똑바로 입력되었는지 확인해야한다.
        if(nameInput.text.Trim() == ""){
            Debug.Log("칸을 채워주세요");
            return;
        }
        LoginVO vo = new LoginVO("Login", nameInput.text);
        string json = JsonUtility.ToJson(vo);
        SocketClient.instance.SendData(json);
    }
}
