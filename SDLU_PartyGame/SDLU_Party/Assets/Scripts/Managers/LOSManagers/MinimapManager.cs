using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapManager : MonoBehaviour
{
    [SerializeField]
    private Material userPointer = null;

    [SerializeField]
    private Material otherPlayerPointer = null;

    void Start()
    {
        StartCoroutine(ShowPosition());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator ShowPosition(){
        var color = userPointer.GetColor("_Color");
        var oColor = otherPlayerPointer.GetColor("_Color");
        yield return new WaitForSeconds(4f);
        while(true){
            Debug.Log("ShowPosition");
            userPointer.SetColor("_Color", Color.red);
            otherPlayerPointer.SetColor("_Color", Color.red);
            yield return new WaitForSeconds(4f);
            userPointer.SetColor("_Color", color);
            otherPlayerPointer.SetColor("_Color", oColor);
            yield return new WaitForSeconds(10f);
        }
    }
}
