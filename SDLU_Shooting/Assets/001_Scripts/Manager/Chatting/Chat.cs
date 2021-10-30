using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chat : MonoBehaviour
{
    static public Chat Instance { get; private set; }

    [SerializeField]
    private GameObject Inputchatting = null;
    [SerializeField]
    private GameObject chattingScroll = null;
    [SerializeField]
    private GameObject chatPref = null;

    private InputField chatInput = null;
    private Text fieldHolder = null;

    private string holderText = "ÀüÃª";

    private bool chatScrollActive = false;
    private bool chatInputActive = false;
    private byte chatType = 0;
    private byte scrollOnTime;

    private void Awake()
    {
        Instance ??= this;
    }

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetChatActive();
        }
    }

    private void SetChatActive()
    {
        MouseManager.MouseLocked = !MouseManager.MouseLocked;
        ChatToggle();
        SetScrollActive(true);
        if (!chatInputActive)
        {
            DataVO vo = new DataVO("chat", chatInput.text);
            SocketClient.Instance.Send(vo);
            CreateChatPref(chatInput.text);
            chatInput.Select();
            chatInput.text = null;
        }
        else
        {
            chatInput.ActivateInputField();
            scrollOnTime = 10;
            //StartCoroutine(CheckScrollActive());
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

    private void SetScrollActive(bool b)
    {
        chatScrollActive = b;
        chattingScroll.SetActive(chatScrollActive);
    }

    private void ChatToggle()
    {
        chatInputActive = !chatInputActive;
        Inputchatting.SetActive(chatInputActive);
        scrollOnTime = 5;
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

    private IEnumerator CheckScrollActive()
    {
        while (true)
        {
            scrollOnTime--;
            yield return new WaitForSeconds(1f);
            if (scrollOnTime == 0)
            {
                SetScrollActive(false);
                break;
            }
        }
    }

    public void CreateChatPref(string str, bool me = true)
    {
        GameObject newChat = Instantiate(chatPref, chattingScroll.transform.GetChild(0).GetChild(0));
        if (me)
        {
            newChat.GetComponent<Text>().text = $"me: {str}";
        }
        else
        {
            newChat.GetComponent<Text>().text = $"{str}";
        }
    }
}
