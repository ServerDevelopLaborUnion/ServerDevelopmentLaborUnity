using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float deletTime = 5f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, deletTime);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 a = transform.up * speed;
        GetComponent<Rigidbody2D>().velocity = a;
    }
}
