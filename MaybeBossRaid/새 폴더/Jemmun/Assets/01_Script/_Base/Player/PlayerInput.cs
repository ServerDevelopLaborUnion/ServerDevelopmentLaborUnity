using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private SkillBase skill = null;

    private void Awake()
    {
        skill = GetComponent<SkillBase>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            SetTarget();
    }


    private void SetTarget()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 0.0f);

        if (hit)
        {
            Debug.Log(hit.transform.name);
            skill.selectedTarget = hit.transform.gameObject;
        }
    }
}
