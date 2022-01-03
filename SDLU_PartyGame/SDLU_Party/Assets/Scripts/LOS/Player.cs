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

    public bool isPlaying = false;

    public bool isMove = false;

    protected bool isDead = false;

    protected Ray ray;
    protected RaycastHit hit;

    private Coroutine moveCoroutine = null;

    private Vector3 diff = Vector3.zero;
    protected MeshRenderer meshRenderer = null;
    private float rotation;

    #region 이벤트
    protected virtual void Awake() {
        move += () => { };
    }
    private void OnEnable() {

        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = true;
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
            if (!isPlaying) yield break;
            Rotate(transform , hit.point);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(hit.point.x, transform.position.y, hit.point.z), speed * Time.deltaTime);
            isMove = transform.position.x != target.x && transform.position.z != target.z;
            yield return null;
        }
    }

    protected virtual void Dead(){
        isDead = true;
        gameObject.SetActive(!isDead);
    }

    public void Rotate(Transform objectPos, Vector3 hitPos){
        diff = hitPos - objectPos.position;
        diff.Normalize();
        rotation = Mathf.Atan2(diff.x, diff.z) * Mathf.Rad2Deg;
        objectPos.eulerAngles = new Vector3(0f, rotation, 0f);
        //transform.rotation = Quaternion.AngleAxis(rotation, transform.position);
    }
}
