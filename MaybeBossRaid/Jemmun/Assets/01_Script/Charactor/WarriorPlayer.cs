using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorPlayer : CharactorBase
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDamage(int damage)
    {
        hp -= damage;
        Debug.Log($"Hit, hp: {hp}, damage: {damage}");
    }
}
