using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorMove : MonoBehaviour
{
    private CharactorInput input = null;
    private Rigidbody rigid = null;

    [Header("이동 속도")]
    [SerializeField] private float speed = 2.0f;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        input = GetComponent<CharactorInput>();
    }

    private void Update()
    {
        if(input.Run)
        {
            speed *= 1.2f; // TODO : 바꿔야 함
        }
        else
        {
            speed *= 0.8f; // TODO : 바꿔야 함
        }

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
