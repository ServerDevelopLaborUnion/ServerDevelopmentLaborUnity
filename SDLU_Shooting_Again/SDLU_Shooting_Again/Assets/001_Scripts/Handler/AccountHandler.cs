using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AccountHandler : MonoBehaviour
{
    [SerializeField] private InputField idInput = null;
    [SerializeField] private InputField pwInput = null;
    [SerializeField] private InputField conPwInput = null;
    [SerializeField] private Button registerBtn = null;
    [SerializeField] private Button loginBtn = null;

    [SerializeField] private GameObject LoginCanvas = null;

    private void Awake()
    {
        // registerBtn.onClick.AddListener(() =>
        // {
        //     if(pwInput.text == conPwInput.text)
        //         SocketClient.Instance.Send(new DataVO("register", JsonUtility.ToJson(new AccountVO(idInput.text, pwInput.text))));
        // });
        // loginBtn.onClick.AddListener(() => 
        // {
        //     SocketClient.Instance.Send(new DataVO("login", JsonUtility.ToJson(new AccountVO(idInput.text, pwInput.text))));
        // });
    }
    private void Start()
    {
        BufferHandler.Instance.AddHandler("loginSuccess", (record) => {
            LoginManager.Instance.GotLogined();
            // LoginCanvas.SetActive(false);
            MouseLockManager.Instance.LockMouse();
        });
    }
}
