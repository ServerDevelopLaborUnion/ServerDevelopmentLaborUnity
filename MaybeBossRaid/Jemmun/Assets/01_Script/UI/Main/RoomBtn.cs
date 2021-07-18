using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomBtn : MonoBehaviour
{
    private Button btnConnect = null;

    void Start()
    {
        btnConnect = GetComponent<Button>();
        btnConnect.onClick.AddListener(ConnectToRoom);

    }

    private void ConnectToRoom()
    {

    }

    
    void Update()
    {
        
    }
}
