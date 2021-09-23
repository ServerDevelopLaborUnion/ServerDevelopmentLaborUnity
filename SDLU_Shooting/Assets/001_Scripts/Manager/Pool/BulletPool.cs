using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Bullet Pool Manager
// Should be added on every Guns=>Fireposition?

public class BulletPool : MonoBehaviour
{
    static public BulletPool inst = null; // for static functions

    [SerializeField] private GameObject bulletPrefab = null;

    [Header("Bullet instance count")]
    [SerializeField] private int instnaceCount = 31; // common mag bullet count

    private List<GameObject> pool = new List<GameObject>();

    private void Awake()
    {
        inst = this; // TODO : 잠만 이거 뭔가 문제가 있을 거 같은데
    }

    private void Start()
    {
        InitPool();
    }

    private void InitPool()
    {
        for(int i = 0; i < instnaceCount; ++i)
        {
            pool.Add(MakeBullet());
        }
    }

    ///<summary>
    /// Instanciates bullet
    ///</summary>
    /// <returns>Instance of bullet based bulletPrefab</returns>
    private GameObject MakeBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, GameManager.instance.player.rotation, transform);
        
        bullet.SetActive(false);

        return bullet;
    }


    ///<summary>
    /// Returns bullet object
    ///</summary>
    static public GameObject Get() // TODO : 요청한 오브젝트의 좌표와 FirePosition 을 넘겨 줘야 할거 같은데
    {
        // This code finds not activated gameobject. When null, it means there is no Object passes the condition.
        GameObject bullet = inst.pool.Find(x => !x.activeSelf);

        if(bullet == null) // when every bullets in pool are active.
        {
            bullet = inst.MakeBullet();
            inst.pool.Add(bullet);
        }

        bullet.SetActive(true);

        bullet.transform.rotation = Quaternion.Euler(GameManager.instance.player.rotation.eulerAngles.x + 90.0f, // else, bullet will look up not front.
                                                     GameManager.instance.player.rotation.eulerAngles.y,
                                                     GameManager.instance.player.rotation.eulerAngles.z);

        bullet.transform.position = inst.transform.position;
        bullet.GetComponent<TrailRenderer>().Clear();

        return bullet;
    }


}
