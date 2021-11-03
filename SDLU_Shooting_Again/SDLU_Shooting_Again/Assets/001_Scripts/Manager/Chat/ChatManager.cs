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
            chattingScroll.SetActive(false);
            Inputchatting.SetActive(false);
        }
    }

    private void SetChatActive()
    {
        if (chatScrollActive == false && chatInputActive == false)
        {
            SetScrollOn();
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

    private void SetScrollOn()
    {
        chatScrollActive = true;
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
            DataVO vo = new DataVO("chat", chatInput.text);
            SocketClient.Instance.Send(vo);
            CreateChatPref(chatInput.text);
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

        if (chattingScroll.transform.GetChild(0).GetChild(0).childCount > 15)
        {
            Destroy(chattingScroll.transform.GetChild(0).GetChild(0).GetChild(0));
        }
    }
}
