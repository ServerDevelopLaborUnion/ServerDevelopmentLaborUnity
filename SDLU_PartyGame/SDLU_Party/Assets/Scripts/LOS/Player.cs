using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private bool isMove = false;
    private bool isAttack = false;

    private Ray ray;
    RaycastHit hit;

    Coroutine moveCoroutine = null;

    void Update()
    {
        if(Input.GetMouseButton(1)){
            Debug.Log("Right mouse button clicked");
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit)){
                Debug.Log(hit.point);
                if (moveCoroutine != null) StopCoroutine(moveCoroutine);
                moveCoroutine = StartCoroutine(Move(hit));
            }
        }
        if(Input.GetKeyDown(KeyCode.A)){
            isAttack = true;
        }
        if(isAttack){
            if(Input.GetMouseButton(0)){
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray, out hit)&&hit.collider.tag=="Enemy"){
                    Debug.Log(hit.point);
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }

    private IEnumerator Move(RaycastHit hit){
        Vector3 target = hit.point;
        isMove = true;
        while(isMove){
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(hit.point.x, transform.position.y, hit.point.z), speed * Time.deltaTime);
            isMove = transform.position.x!=target.x&&transform.position.z!=target.z;
            yield return null;
        }
    }
}
