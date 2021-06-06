
// 뭔가에 맞았을 때 일어나는 것들을 위했음
/*
데미지는 데미지이다
스킬은 우리팀에게서 오는 버프일겁니다

생각 하나도 안 하고 써서 뭔가가 확실히 잘못됬어
흠흠

- 우엽
*/

interface IHit
{
    void OnDamage(int damage);
    void OnSkillHit(Classes classEnum, SkillList skillEnum);
}