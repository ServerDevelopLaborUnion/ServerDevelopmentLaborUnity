using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : CharactorBase
{
    private void Setvariable(){
        hp = 80;
        mp = 120;
        pos = 0;
        atk = 1.25f;
        def = 1.2f;
    }
    public override void OnDamage(int damage)
    {
        hp -= damage;
        Debug.Log($"Hit, hp: {hp}, damage: {damage}");
    }
}
