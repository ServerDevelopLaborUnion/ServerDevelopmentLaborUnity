using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    #region Action
    public event Action move;
    public Action getMove {
        get{
            return Move;
        }
    }
    #endregion

    [SerializeField]
    private float speed;

    protected bool isMove = false;

    protected Ray ray;
    protected RaycastHit hit;

    private Coroutine moveCoroutine = null;

    private Vector3 diff = Vector3.zero;
    private float rotation;

    #region 이벤트
    protected virtual void Awake() {
        move += () => { };
    }
    private void OnEnable() {
        Initvalue();
    }
    protected virtual void Update()
    {
        move();
    }
    #endregion

    protected virtual void Initvalue(){
        move += Move;
    }
    
    private void Move()
    {
        if (Input.GetMouseButton(1))
        {
            Debug.Log("Right mouse button clicked");
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
            {
                Debug.Log(hit.point);
                if (moveCoroutine != null) StopCoroutine(moveCoroutine);
                moveCoroutine = StartCoroutine(Move(hit));
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            isMove = false;
        }
    }
    private IEnumerator Move(RaycastHit hit)
    {
        Vector3 target = hit.point;
        isMove = true;
        while (isMove)
        {
            Rotate(hit.point);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(hit.point.x, transform.position.y, hit.point.z), speed * Time.deltaTime);
            isMove = transform.position.x != target.x && transform.position.z != target.z;
            yield return null;
        }
    }

    private void Rotate(Vector3 hitPos){
        diff = hitPos - transform.position;
        diff.Normalize();
        rotation = Mathf.Atan2(diff.x, diff.z) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0f, rotation, 0f);
        //transform.rotation = Quaternion.AngleAxis(rotation, transform.position);
    }
}
