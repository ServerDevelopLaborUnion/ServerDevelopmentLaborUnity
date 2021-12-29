using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOSPlayer : Player
{
    private AttackDistance attackDistance = null;

    protected override void Awake()
    {
        base.Awake();
        attackDistance = gameObject.transform.GetChild(0).GetComponent<AttackDistance>();
    }

    protected override void Update()
    {
        base.Update();
        if(Input.GetKey(KeyCode.A)){
            attackDistance.SetActive(true);
            Attack();
        }
        else{
            attackDistance.SetActive(false);
        }
    }

    

    private void Attack(){
        if(Input.GetMouseButton(0)){
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit)&&hit.collider.tag=="Enemy"&&Vector3.Distance(transform.position, hit.collider.transform.position) < 7.5f){
                Destroy(hit.collider.gameObject);
            }
        }
    }
}
