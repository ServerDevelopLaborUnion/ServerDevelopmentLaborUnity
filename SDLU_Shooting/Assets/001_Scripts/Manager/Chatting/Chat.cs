using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chat : MonoBehaviour
{
    [SerializeField]
    private GameObject chatting = null;
    
    private GameObject chattingScroll = null;

    private InputField chatInput = null;
    private Text fieldHolder = null;

    private string holderText = "ÀüÃª";

    private bool chatActive = false;
    private byte chatType = 0;

    void Start()
    {
        chatInput = chatting.gameObject.transform.GetChild(1).GetComponent<InputField>();
        fieldHolder = chatting.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Text>();
        chattingScroll = chatting.gameObject.transform.GetChild(0).GetComponent<GameObject>();
        fieldHolder.text = holderText;
        chatting.SetActive(false);
        Debug.Log(chattingScroll);
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
        MouseManager.MouseLocked = !MouseManager.MouseLocked;
        ChangeChatActive();
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

    public void ChangeChatActive()
    {
        chatActive = !chatActive;
        chatting.SetActive(chatActive);
    }

    private IEnumerator FadeChat()
    {
        chattingScroll.SetActive(true);
        yield return new WaitForSeconds(3f);
        chattingScroll.SetActive(false);
    }
}
