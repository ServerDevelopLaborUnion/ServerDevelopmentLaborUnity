using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectBtnManger : MonoBehaviour
{
    [SerializeField] private Button btnReload = null;

    void Start()
    {
        btnReload = GetComponent<Button>();
        btnReload.onClick.AddListener(ReloadRoomStat);

        ReloadRoomStat();
    }

    private void ReloadRoomStat()
    {

    }
    
    void Update()
    {
        
    }
}
