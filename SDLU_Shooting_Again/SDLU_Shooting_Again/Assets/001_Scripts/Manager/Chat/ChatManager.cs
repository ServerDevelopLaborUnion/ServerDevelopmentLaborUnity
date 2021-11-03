using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoSingleton<ChatManager>
{
    [SerializeField]
    private GameObject Inputchatting = null;
    [SerializeField]
    private GameObject chattingScroll = null;
    [SerializeField]
    private GameObject chatPref = null;

    private InputField chatInput = null;
    
    [SerializeField]
    private Scrollbar chattingSlider = null;

    private bool chatScrollActive = false;
    private bool chatInputActive = false;

    void Start()
    {
        chatInput = Inputchatting.GetComponent<InputField>();
        Inputchatting.SetActive(chatInputActive);
        chattingScroll.SetActive(chatScrollActive);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SetChatActive();
        }
        if (Input.GetKey(KeyCode.RightShift) && Input.GetKey(KeyCode.Return))
        {
            ScrollToggle(false);
            Inputchatting.SetActive(false);
            chatInputActive = false;
        }
    }

    private void SetChatActive()
    {
        if (chatScrollActive == false && chatInputActive == false)
        {
            ScrollToggle(true);
            SetInputToggle();
        }
        else if(chatScrollActive == false && chatInputActive == true)
        {
            Debug.LogError("뭔가 이상한데??");
        }
        else
        {
            SetInputToggle();
        }
    }

    private void ScrollToggle(bool b)
    {
        chatScrollActive = b;
        chattingScroll.SetActive(chatScrollActive);
    }

    private void SetInputToggle()
    {
        chatInputActive = !chatInputActive;
        Inputchatting.SetActive(chatInputActive);
        if (chatInputActive)
        {
            chatInput.ActivateInputField();
        }
        else
        {
            if (chatInput.text != null)
            {
                SocketClient.Instance.Send(new DataVO("chat", chatInput.text));
            }
            chatInput.Select();
            chatInput.text = null;
        }
    }

    public void CreateChatPref(string msg, bool me = true)
    {
        GameObject newChat = Instantiate(chatPref, chattingScroll.transform.GetChild(0).GetChild(0));
        if (me)
        {
            newChat.GetComponent<Text>().text = $"me: {msg}";
        }
        else
        {
            newChat.GetComponent<Text>().text = $"{msg}";
        }
        chattingSlider.value = 0;
    }
}
