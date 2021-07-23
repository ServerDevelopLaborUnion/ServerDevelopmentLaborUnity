using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class JobManager : MonoBehaviour
{
    // 각 버튼에 해당하는 fader 인덱스는 같음
    [SerializeField] private Button[] btnJobs = new Button[5];
    [SerializeField] private GameObject[] faders = new GameObject[5];

    // 지난 경험
    UnityEngine.Events.UnityAction btnEvent;

    static private JobManager instance = null; // Singleton

    private bool bSelected = false; // 직업 선택 메세지 받은 경우
    private Queue<JobVO> jobQueue = new Queue<JobVO>(); // 처리 대기열

    private object lockObj = new object(); // 락

    private void Awake()
    {
        instance = this;

        #region NULLCheck
        foreach (Button btn in btnJobs)
        {
            NullChecker.CheckNULL(btn, true);
        }
        foreach (GameObject fader in faders)
        {
            NullChecker.CheckNULL(fader, true);
        }
        #endregion // NULLCheck

        for (int i = 0; i < btnJobs.Length; ++i)
        {
            int idx = i;
            btnEvent = () => { SelectJob((JobList)idx); };

            btnJobs[i].onClick.AddListener(btnEvent);
            faders[i].SetActive(false);
        }
    }

    // 직업 선택
    private void SelectJob(JobList job)
    {
        JobVO jobVO = new JobVO(job, true);

        DataVO vo = new DataVO("jobselect", JsonUtility.ToJson(jobVO));

        faders[(int)job].SetActive(true);

        SocketClient.Send(JsonUtility.ToJson(vo));

        SetSelected(jobVO);
    }




    // 직업 선택됨으로 표시, 버튼 인터렉스 삭제
    private void SetSelected(JobVO vo)
    {
        for (int i = 0; i < btnJobs.Length; ++i)
        {
            btnJobs[i].interactable = false;
        }

        faders[(int)vo.job].SetActive(true);
    }

    private void HandleSelected(JobVO vo)
    {
        btnJobs[(int)vo.job].interactable = false;
    }

}

public partial class JobManager : MonoBehaviour
{
    static public void HandleVO(JobVO vo)
    {
        lock (instance.lockObj)
        {
            instance.bSelected = true;
            instance.jobQueue.Enqueue(vo);
        }
    }


    private void FixedUpdate()
    {
        if (bSelected)
        {
            bSelected = false;

            JobVO vo;
            lock (lockObj)
            {
                vo = jobQueue.Dequeue();
            }
            HandleSelected(vo);
        }
    }
}