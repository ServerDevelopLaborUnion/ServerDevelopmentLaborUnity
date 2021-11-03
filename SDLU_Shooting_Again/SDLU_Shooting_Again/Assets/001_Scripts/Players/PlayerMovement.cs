using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float rotateSpeed;
    [SerializeField] float speed = 5.0f;

    private Rigidbody rigid = null;

    private Vector3 rotation = new Vector3(0, 0, 0);

    private void Start()
    {
        // Cursor.visible = false;
        // Cursor.lockState = CursorLockMode.Locked;

        rigid = GetComponent<Rigidbody>();


        InputManager.Instance.OnKeyFoward += () => {
            rigid.AddForce(transform.forward * speed * Time.deltaTime, ForceMode.Impulse);
        };

        InputManager.Instance.OnKeyBackWard += () => {
            rigid.AddForce(-transform.forward * speed * Time.deltaTime, ForceMode.Impulse);
        };

        InputManager.Instance.OnKeyLeft += () => {
            rigid.AddForce(-transform.right * speed * Time.deltaTime, ForceMode.Impulse);
        };

        InputManager.Instance.OnKeyRight += () => {
            rigid.AddForce(transform.right * speed * Time.deltaTime, ForceMode.Impulse);
        };

        InputManager.Instance.OnKeyDown += () => {
            rigid.AddForce(-transform.up * speed * Time.deltaTime, ForceMode.Impulse);
        };

        InputManager.Instance.OnKeyUp += () => {
            rigid.AddForce(transform.up * speed * Time.deltaTime, ForceMode.Impulse);
        };

        InputManager.Instance.OnKeyRollLeft += () => {
            rotation.z = rotateSpeed * Time.deltaTime;
        };

        InputManager.Instance.OnKeyRollRight += () => {
            rotation.z = -rotateSpeed * Time.deltaTime;
        };
    }

    private void Update()
    {
        rotation.y = Input.GetAxis("Mouse X") * InputManager.Instance.GetMouseSensitivity();
        rotation.x = -Input.GetAxis("Mouse Y") * InputManager.Instance.GetMouseSensitivity();

        rigid.rotation *= Quaternion.Euler(rotation);
        rotation.z = 0;
    }



    // private void Rotation()
    // {
    //     float x = Input.GetAxis("Mouse X") * OptionInput.instance.mouseSensitivity;
    //     float y = Input.GetAxis("Mouse Y") * OptionInput.instance.mouseSensitivity;
    //     float z = 0;

    //     if (input.RollLeft)
    //     {
    //         z = rotateSpeed * Time.deltaTime;
    //     }
    //     if (input.RollRight)
    //     {
    //         z = -rotateSpeed * Time.deltaTime;
    //     }

    //     rigid.rotation *= Quaternion.Euler(new Vector3(-y, x, z));
    // }

}
