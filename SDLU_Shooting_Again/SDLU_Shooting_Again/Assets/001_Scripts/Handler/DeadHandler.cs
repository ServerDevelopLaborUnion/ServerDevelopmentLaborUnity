using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] spawns;
    [SerializeField] private Mesh cube;
    [SerializeField] private Mesh cabsule;

    #region 이벤트
    private void Start()
    {
        BufferHandler.Instance.AddHandler("dead", (data) =>
        {
            DamageVO vo = JsonUtility.FromJson<DamageVO>(data);

            // StartCoroutine(DeadMotion(data));
            Destroy(UserManager.Instance.Get(vo.id).gameObject);
            UserManager.Instance.Remove(vo.id);
            if(vo.id == GameManager.Instance.Player.ID)
            {
                Application.Quit();

            }
        });
    }
    #endregion

    private IEnumerator DeadMotion(string data)
    {
        GameManager.Instance.Player.GetComponent<MeshFilter>().mesh = cube;
        yield return new WaitForSeconds(10f);
        GameManager.Instance.Player.GetComponent<MeshFilter>().mesh = cabsule;
        int rand = UnityEngine.Random.Range(0, spawns.Length);
        DeadVO vo = JsonUtility.FromJson<DeadVO>(data);

        if (vo.id == GameManager.Instance.Player.ID)
        {
            GameManager.Instance.Player.transform.position = spawns[rand].transform.position;
        }
    }
}
