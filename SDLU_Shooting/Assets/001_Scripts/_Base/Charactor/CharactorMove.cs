using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CharactorMove : MonoBehaviour
{
    private CharactorInput input = null;
    private Rigidbody rigid = null;

    [Header("이동 속도")]
    [SerializeField] private float speed = 2.0f;

    // [Header("최대 속도")]
    // [SerializeField] private float maxVelocity = 10.0f;

    [Header("회전 속도")]
    [SerializeField] private float rotateSpeed = 2.0f;



    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        input = GetComponent<CharactorInput>();
    }

    private void Update()
    {
        Move();
        Rotation();
        //ClipVecocity();
    }



    // private void ClipVecocity() // 가속도 제한
    // {
    //     rigid.velocity = Vector3.ClampMagnitude(rigid.velocity, maxVelocity); // TODO : 속도가 낮을 때 클램프
    // }


}

public partial class CharactorMove : MonoBehaviour
{
    private void Rotation()
    {
        float x = Input.GetAxis("Mouse X") * OptionInput.instance.mouseSensitivity;
        float y = Input.GetAxis("Mouse Y") * OptionInput.instance.mouseSensitivity;
        float z = 0;

        if(input.RollLeft)
        {
            z = rotateSpeed * Time.deltaTime;
        }
        if(input.RollRight)
        {
            z = -rotateSpeed * Time.deltaTime;
        }

        rigid.rotation *= Quaternion.Euler(new Vector3(-y, x, z));
    }
}

// 이동 함수들
public partial class CharactorMove : MonoBehaviour
{
     private void Move()
     {
        if(input.Foward)
        {
            Foward();
        }
        if(input.Backward)
        {
            Backward();
        }
        if(input.Right)
        {
            Right();
        }
        if(input.Left)
        {
            Left();
        }

        if(input.Up)
        {
            Up();
        }
        if(input.Down)
        {
            Down();
        }
     }



    // 이동 함수들
    private void Foward()
    {
        rigid.AddForce(transform.forward * speed * Time.deltaTime, ForceMode.Impulse);
    }
    private void Backward()
    {
        rigid.AddForce(-transform.forward * speed * Time.deltaTime, ForceMode.Impulse);
    }
    private void Right()
    {
        rigid.AddForce(transform.right * speed * Time.deltaTime, ForceMode.Impulse);
    }
    private void Left()
    {
        rigid.AddForce(-transform.right * speed * Time.deltaTime, ForceMode.Impulse);
    }

    private void Up()
    {
        rigid.AddForce(transform.up * speed * Time.deltaTime, ForceMode.Impulse);
    }
    private void Down()
    {
        rigid.AddForce(-transform.up * speed * Time.deltaTime, ForceMode.Impulse);
    }
}