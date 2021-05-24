using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;
    public float rotateSpeed = 20f;

    private PlayerInput playerInput;
    private Rigidbody2D rigidbody2d;

    private Vector3 moveDirection;
    private float rotateDir;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        moveDirection = (Vector2.up * playerInput.frontMove).normalized;
        moveDirection += (Vector3.right * playerInput.rightMove).normalized;

        transform.rotation *= Quaternion.Euler(0, 0, -playerInput.rightMove * rotateSpeed * Time.deltaTime);
    }

    private void Rotate()
    {
        Vector3 target = playerInput.mousePos;
        Vector3 v = target - transform.position;

        float degree = Mathf.Atan2(v.x, v.y) * Mathf.Rad2Deg;
        float rot = Mathf.LerpAngle(transform.eulerAngles.z, -degree, Time.deltaTime * rotateSpeed);

        transform.eulerAngles = new Vector3(0, 0, rot);
    }

    void FixedUpdate()
    {
        rigidbody2d.velocity = moveDirection * speed;
    }
}
