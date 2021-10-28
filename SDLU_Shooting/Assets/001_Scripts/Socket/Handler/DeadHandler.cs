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
            StartCoroutine(DeadMotion(data));

        });
    }
    #endregion

    private IEnumerator DeadMotion(string data)
    {
        GameManager.instance.playerBase.GetComponent<MeshFilter>().mesh = cube;
        yield return new WaitForSeconds(10f);
        GameManager.instance.playerBase.GetComponent<MeshFilter>().mesh = cabsule;
        int rand = UnityEngine.Random.Range(0, spawns.Length);
        DeadVO vo = JsonUtility.FromJson<DeadVO>(data);

        if (vo.id == GameManager.instance.playerBase.ID)
        {
            GameManager.instance.player.transform.position = spawns[rand].transform.position;
        }
    }
}
