public interface IDamageable
{
    // damage �� ���� ���� �ſ���.
    // ���߿� Ȯ���� ������ �� �� ��
    public void OnDamage(int damage);

    // �Լ��� ��޵Ǽ� interface �� �� �� �־��.
    public int ID { get; set; }
}