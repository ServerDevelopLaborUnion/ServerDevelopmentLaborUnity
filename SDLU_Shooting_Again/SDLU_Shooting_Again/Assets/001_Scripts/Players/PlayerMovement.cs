using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float rotateSpeed;

    private Rigidbody rigid = null;

    private Vector3 rotation = new Vector3(0, 0, 0);

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        rigid = GetComponent<Rigidbody>();


        InputManager.Instance.OnKeyFoward += () => {
            // rigid.AddForce();
        };

        InputManager.Instance.OnKeyBackWard += () => {

        };

        InputManager.Instance.OnKeyLeft += () => {

        };

        InputManager.Instance.OnKeyRight += () => {

        };

        InputManager.Instance.OnKeyDown += () => {

        };

        InputManager.Instance.OnKeyUp += () => {

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
