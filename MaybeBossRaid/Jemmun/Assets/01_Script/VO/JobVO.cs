using System;

[Serializable]
public class JobVO
{
    public JobList job; // 누른 직업
    public bool selected; // 선택 여부

    public JobVO(JobList job, bool selected)
    {
        this.job = job;
        this.selected = selected;
    }
}
