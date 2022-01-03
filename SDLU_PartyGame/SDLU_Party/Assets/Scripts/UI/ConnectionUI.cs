using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConnectionUI : MonoBehaviour
{
    [SerializeField] private Button btnConnect;

    private void Start()
    {
        string payload = JsonUtility.ToJson(new { });

        btnConnect.onClick.AddListener(() => {
            SocketClient.Instance.Connect();
            //SocketClient.Instance.Send(new DataVO("GetRoomData", payload));
        });
    }
}
