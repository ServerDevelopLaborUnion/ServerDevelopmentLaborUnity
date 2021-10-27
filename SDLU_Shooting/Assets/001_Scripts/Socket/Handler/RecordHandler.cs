using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordHandler : MonoBehaviour
{
    [SerializeField] private Text killTxt = null;
    [SerializeField] private Text deathTxt = null;
    [SerializeField] private Button testBtn = null;
    [SerializeField] private Button testBtn2 = null;
    private void Awake()
    {
        testBtn.onClick.AddListener(() =>
        {
            ReadFromDB();
        });
        testBtn2.onClick.AddListener(() =>
        {
            RecordToDB(new RecordVO(10, 10, 10));
        });
        
    }
    private void Start()
    {
        BufferHandler.Instance.AddHandler("read", (record) => {
            RecordVO vo = JsonUtility.FromJson<RecordVO>(record);
            SetUiOnRecord(vo);
            Debug.Log(record);
        });
    }
    public void RecordToDB(RecordVO vo)
    {
        SocketClient.Instance.Send(new DataVO("record", JsonUtility.ToJson(vo)));
    }

    public void ReadFromDB()
    {
        SocketClient.Instance.Send(new DataVO("read", ""));
    }

    private void SetUiOnRecord(RecordVO vo)
    {
        killTxt.text = vo.kill.ToString();
        deathTxt.text = vo.death.ToString();
    }
}
