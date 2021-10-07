using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    void Start()
    {
        BufferHandler.Instance.AddHandler("damage", (msg) => {
            Debug.Log(msg);
        });

    }
}
