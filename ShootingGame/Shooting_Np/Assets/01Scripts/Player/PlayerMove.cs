using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;
    public float rotateSpeed = 20f;
    public float damage = 1f;

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
        Shooting();
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

    private void Shooting()
    {
        if (playerInput.fire)
        {
            int myId = GameManager.instance.myId;
            ShootVO vo = new ShootVO(myId, transform.rotation.eulerAngles);
            string payload = JsonUtility.ToJson(vo);

            DataVO dataVO = new DataVO();
            dataVO.type = "Shoot";
            dataVO.payload = payload;
            SocketClient.instance.SendData(JsonUtility.ToJson(dataVO)); //json으로 변경해서 전송
        }
    }

    void FixedUpdate()
    {
        rigidbody2d.velocity = moveDirection * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            Debug.Log("나맞았어요 서버에 전송" + GameManager.instance.myId);

            int myId = GameManager.instance.myId;
            OnDamageVO vo = new OnDamageVO(myId, damage);
            string payload = JsonUtility.ToJson(vo);

            DataVO dataVO = new DataVO();
            dataVO.type = "OnDamage";
            dataVO.payload = payload;
            SocketClient.instance.SendData(JsonUtility.ToJson(dataVO)); //json으로 변경해서 전송
        }
    }
}
