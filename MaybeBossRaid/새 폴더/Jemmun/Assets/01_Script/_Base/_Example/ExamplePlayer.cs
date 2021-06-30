using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamplePlayer : CharactorBase
{

    private void Awake()
    {
        
    }

    public override void OnDamage(int damage)
    {
        hp -= damage;
        Debug.Log($"Hit, hp: {hp}, damage: {damage}");
    }


}
