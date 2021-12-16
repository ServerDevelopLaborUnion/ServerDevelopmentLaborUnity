using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private bool isMove = false;

    private Ray ray;
    RaycastHit hit;

    void Update()
    {
        if(Input.GetMouseButton(1)){
            Debug.Log("Right mouse button clicked");
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit)){
                Debug.Log(hit.point);
                StartCoroutine(Move(hit));
            }
        }
    }

    private IEnumerator Move(RaycastHit hit){
        Vector3 target = hit.point;
        if(!isMove){
            isMove = true;
        }else{
            isMove = false;
        }

        while(isMove){
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(hit.point.x, transform.position.y, hit.point.z), speed * Time.deltaTime);
            isMove = transform.position.x!=target.x&&transform.position.z!=target.z;
            yield return null;
        }
        isMove = false;
    }
}
