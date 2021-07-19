// 스킬의 정보를 담고 있는 클레스
public class SkillData
{
    public delegate void SkillHitCallback(CharactorBase targetBase);

    public readonly string name;
    public readonly string info;
    public readonly int mpCost;
    public readonly int damage;
    public readonly SkillHitCallback skillHitCallback;


    public SkillData(string name, string info, int mpCost, int damage, SkillHitCallback skillHitCallback)
    {
        this.name = name;
        this.info = info;
        this.mpCost = mpCost;
        this.damage = damage;
        this.skillHitCallback = skillHitCallback;
    }
}