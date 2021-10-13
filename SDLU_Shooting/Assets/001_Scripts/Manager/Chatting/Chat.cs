using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chat : MonoBehaviour
{
    [SerializeField]
    private GameObject chatting = null;

    private InputField chatInput = null;

    private bool chatActive = false;

    void Start()
    {
        chatting.SetActive(chatActive);
        chatInput = chatting.gameObject.transform.GetChild(1).GetComponent<InputField>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SetChatActive();
        }
    }

    private void SetChatActive()
    {
        string msg = chatInput.text;
        chatActive = !chatActive;
        chatting.SetActive(chatActive);
        MouseManager.MouseLocked = !MouseManager.MouseLocked;
        if (!chatActive)
        {
            //DataVO vo = new DataVO("msg", GameManager.instance.playerBase.ID, msg);
            //SocketClient.Instance.Send(vo);
            chatInput.Select();
            chatInput.text = null;
        }
        else
        {
            chatInput.ActivateInputField();
        }
    }

    public void RecvChat()
    {

    }
}
