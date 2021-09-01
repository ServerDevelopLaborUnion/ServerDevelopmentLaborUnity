using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manages reaction

public class ReactionManager : MonoBehaviour
{
    static private ReactionManager inst = null; // for static funcions

    private void Awake()
    {
        inst = this;
    }

    ///<summary>
    ///Adds reaction force to target,
    ///default force direction is back.
    ///</summary>
    ///<param name="target">target you want to add reaction force</param>
    ///<param name="force">reaction force</param>
    ///<param name="decreaseAmount">reaction force / decreaseAmount</param>
    static public void Reaction(Rigidbody target, float force, float decreaseAmount = 1.0f)
    {
        target.AddForce(-target.transform.forward * force / decreaseAmount, ForceMode.Impulse);
    }
}
