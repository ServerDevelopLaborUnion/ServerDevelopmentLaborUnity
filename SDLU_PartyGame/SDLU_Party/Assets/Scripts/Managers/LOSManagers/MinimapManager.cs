using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapManager : MonoBehaviour
{
    [SerializeField]
    private Material otherPlayerPointer = null;

    void Start()
    {
        StartCoroutine(ShowPosition());
    }

    void Update()
    {
        
    }

    private IEnumerator ShowPosition(){
        var oColor = otherPlayerPointer.GetColor("_Color");
        yield return new WaitForSeconds(4f);
        while(true){
            otherPlayerPointer.SetColor("_Color", Color.red);
            yield return new WaitForSeconds(4f);
            otherPlayerPointer.SetColor("_Color", oColor);
            yield return new WaitForSeconds(10f);
        }
    }
}
