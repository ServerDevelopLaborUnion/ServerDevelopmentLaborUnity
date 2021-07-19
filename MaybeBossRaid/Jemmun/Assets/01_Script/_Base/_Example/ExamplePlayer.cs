using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamplePlayer : CharactorBase
{
    // 사실 이거 구현할 필요가 없어진거 같긴한데 음
    public override void OnDamage(int damage)
    {
        hp -= damage;
        Debug.Log($"Hit, hp: {hp}, damage: {damage}");
    }

    // 여기에 특정한 변수가 필요할 시 넣어줘요
}
