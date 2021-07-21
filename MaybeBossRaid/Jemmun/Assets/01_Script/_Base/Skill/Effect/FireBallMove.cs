using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.5f;
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}
