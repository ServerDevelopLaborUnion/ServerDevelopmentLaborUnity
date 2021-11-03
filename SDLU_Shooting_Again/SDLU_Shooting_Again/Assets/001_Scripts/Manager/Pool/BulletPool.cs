using System.Collections;
using System.Collections.Generic;
using UnityEngine;

sealed public class BulletPool : MonoSingleton<BulletPool>
{
    const int DEFAULT_OBJECT_COUNT = 60;

    List<Bullet> pool = new List<Bullet>();

    public Bullet bulletPrefab = null;


    private void Awake()
    {
        InitPool();
    }


    private void InitPool()
    {
        for (int i = 0; i < DEFAULT_OBJECT_COUNT; ++i)
        {
            pool.Add(MakeObject());
        }
    }

    private Bullet MakeObject()
    {
        GameObject obj = Instantiate(bulletPrefab.gameObject, this.transform);
        obj.SetActive(false);
        return obj.GetComponent<Bullet>();
    }

    /// <summary>
    /// 총알을 가져옵니다.<br/>
    /// ## SetActive(false); 인 상태로 반환됨 ##
    /// </summary>
    /// <returns>bullet prefab</returns>
    public Bullet Get()
    {
        Bullet temp = pool.Find(x => !x.gameObject.activeSelf);

        if(temp == null)
        {
            temp = MakeObject();
            pool.Add(temp);
        }

        return temp;
    }

    /// <summary>
    /// 총알 여러개를 한번에 가져올 수 있음
    /// </summary>
    /// <param name="count">수량</param>
    /// <returns>List of bullet prefabs</returns>
    public List<Bullet> Get(int count)
    {
        List<Bullet> tempList = pool.FindAll(x => !x.gameObject.activeSelf);

        switch(tempList.Count.CompareTo(count))
        {
            case -1:
                for (int i = 0; i < tempList.Count - count; ++i)
                {
                    Bullet tempObj = MakeObject();
                    tempList.Add(tempObj);
                    pool.Add(tempObj);
                }
                break;

            case 1:
                tempList.RemoveRange(tempList.Count - count - 1, tempList.Count - count);
                break;

        }

        return tempList;
    }
}
