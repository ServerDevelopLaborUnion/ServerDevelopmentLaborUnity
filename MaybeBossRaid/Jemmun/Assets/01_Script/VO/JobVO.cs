using System;

[Serializable]
public class JobVO
{
    public JobList job; // ���� ����
    public bool selected; // ���� ����

    public JobVO(JobList job, bool selected)
    {
        this.job = job;
        this.selected = selected;
    }
}
