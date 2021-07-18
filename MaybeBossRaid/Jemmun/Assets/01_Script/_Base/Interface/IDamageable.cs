public interface IDamageable
{
    // damage 만 들어가지 않을 거에요.
    // 나중에 확실히 뭔가가 더 들어갈 것
    public void OnDamage(int damage);

    // 함수로 취급되서 interface 에 들어갈 수 있어요.
    public int ID { get; set; }
}