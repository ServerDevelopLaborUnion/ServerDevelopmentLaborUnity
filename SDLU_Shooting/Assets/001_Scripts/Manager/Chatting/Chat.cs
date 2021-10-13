using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chat : MonoBehaviour
{
    [SerializeField]
    private GameObject chatting = null;

    private InputField chatInput = null;
    private Text fieldHolder = null;

    private string holderText = "ÀüÃª";

    private bool chatActive = false;
    private byte chatType = 0;

    void Start()
    {
        chatInput = chatting.gameObject.transform.GetChild(1).GetComponent<InputField>();
        fieldHolder = chatting.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Text>();
        fieldHolder.text = holderText;
        chatting.SetActive(chatActive);
    }

    void Update()
    {
        if (chatActive && Input.GetKeyDown(KeyCode.Tab))
        {
            ChangeChatType();
            switch (chatType)
            {
                case 0:
                    holderText = "ÀüÃÂ";
                    break;
                case 1:
                    holderText = "±Ó¸»";
                    break;
                default:
                    Debug.Log("Not making");
                    holderText = "¿À·ù";
                    break;
            }
            fieldHolder.text = holderText;
        }
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

    private void ChangeChatType()
    {
        chatType++;
        if (chatType > 1 || chatType < 0)
        {
            chatType = 0;
        }
    }

    public void RecvChat()
    {

    }
}
