using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    #region Action

    /// <summary>
    /// Called when left clicked
    /// </summary>
    public event Action OnMouseAttackButtonDown;
    /// <summary>
    /// Called when right clicked
    /// </summary>
    public event Action OnMouseMoveButtonDown;
    /// <summary>
    /// Called when wheel clicked
    /// </summary>
    public event Action OnMouseMiddleDown;

    #endregion

    InputMapping key = new InputMapping();

    void Start()
    {
        OnMouseAttackButtonDown += () => { };
        OnMouseMoveButtonDown   += () => { };
        OnMouseMiddleDown       += () => { };
    }

    void Update()
    {
        if(Input.GetMouseButtonDown((int)key.move))
        {
            OnMouseMoveButtonDown();
        }

        if(Input.GetMouseButtonDown((int)key.attack))
        {
            OnMouseAttackButtonDown();
        }


    }
}
