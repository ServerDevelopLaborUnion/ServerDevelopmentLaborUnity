using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chat : MonoBehaviour
{
    [SerializeField]
    private GameObject Inputchatting = null;
    [SerializeField]
    private GameObject chattingScroll = null;

    private InputField chatInput = null;
    private Text fieldHolder = null;

    private string holderText = "ÀüÃª";

    private bool chatScrollActive = false;
    private bool chatInputActive = false;
    private byte chatType = 0;

    void Start()
    {
        chatInput = Inputchatting.GetComponent<InputField>();
        fieldHolder = Inputchatting.gameObject.transform.GetChild(0).GetComponent<Text>();
        fieldHolder.text = holderText;
        Inputchatting.SetActive(false);
        chattingScroll.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
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
        ChatToggle();
        if (!chatInputActive)
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

    public void ChatToggle()
    {
        chatInputActive = !chatInputActive;
        Inputchatting.SetActive(chatInputActive);

    }

    private IEnumerator FadeChat()
    {
        chattingScroll.SetActive(true);
        for (int i = 0; i < 60; i++)
        {
            var originColor = chattingScroll.GetComponent<Image>().color;
            chattingScroll.GetComponent<Image>().color = originColor + new Color(0, 0, 0, 1 / 60);
            yield return new WaitForEndOfFrame();
        }
        chattingScroll.SetActive(false);
    }
}
