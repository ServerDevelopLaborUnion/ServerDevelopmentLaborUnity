public interface IDamageable
{
    // damage �� ���� ���� �ſ���.
    // ���߿� Ȯ���� ������ �� �� ��
    public void OnDamage(int damage);

    public void OnSkillHit(SkillEnum skillEnum);
}