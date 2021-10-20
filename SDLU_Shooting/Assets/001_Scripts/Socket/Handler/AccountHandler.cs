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

    private void Awake()
    {
        registerBtn.onClick.AddListener(() =>
        {
            SocketClient.Instance.Send(new DataVO("register", JsonUtility.ToJson(new AccountVO(idInput.text, pwInput.text))));
        });
    }
}
