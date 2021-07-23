using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class JobManager : MonoBehaviour
{
    // �� ��ư�� �ش��ϴ� fader �ε����� ����
    [SerializeField] private Button[] btnJobs = new Button[5];
    [SerializeField] private GameObject[] faders = new GameObject[5];

    // ���� ����
    UnityEngine.Events.UnityAction btnEvent;

    static private JobManager instance = null; // Singleton

    private bool bSelected = false; // ���� ���� �޼��� ���� ���
    private Queue<JobVO> jobQueue = new Queue<JobVO>(); // ó�� ��⿭

    private object lockObj = new object(); // ��

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

    // ���� ����
    private void SelectJob(JobList job)
    {
        JobVO jobVO = new JobVO(job, true);

        DataVO vo = new DataVO("jobselect", JsonUtility.ToJson(jobVO));

        faders[(int)job].SetActive(true);

        SocketClient.Send(JsonUtility.ToJson(vo));

        SetSelected(jobVO);
    }




    // ���� ���õ����� ǥ��, ��ư ���ͷ��� ����
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