using System;

[Serializable]
public class SkillVO
{
    public string skill;
    public int id;

    public SkillVO(int id, string skill)
    {
        this.id = id;
        this.skill = skill;
    } 
        
}
