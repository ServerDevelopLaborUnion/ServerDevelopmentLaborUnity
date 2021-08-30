using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootable : MonoBehaviour
{
    [Header("발사체 (또는 총알)")]
    [SerializeField] private GameObject projectile = null; 

    [Header("장전된 수")]
    [SerializeField] private int ammoCount = 0;

    // TODO : 발사체 발사

    protected virtual void Shoot()
    {

    }

    protected virtual void Reload()
    {

    }
}
