// ¹Ú»óºó °³¹ß

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 15f;

    void Update()
    {
        Move();
    }
    private void Move(){
        transform.Translate(Vector2.up* speed *Time.deltaTime);
    }
    
}
