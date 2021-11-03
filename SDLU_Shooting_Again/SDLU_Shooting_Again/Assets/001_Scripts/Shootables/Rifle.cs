using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Shootable
{
    float lastFireTime = float.MinValue;


    protected override void Start()
    {
        base.Start();

        InputManager.Instance.OnMouseFire += () => {
            if(IsShootable() && lastFireTime + fireDelay <= Time.time)
            {
                lastFireTime = Time.time;
                Shoot();
            }
        };
    }
}
