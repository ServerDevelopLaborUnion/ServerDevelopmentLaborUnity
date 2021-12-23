using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapManager : MonoBehaviour
{
    [SerializeField]
    private Material userPointer = null;

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
        yield return new WaitForSeconds(4f);
        while(true){
            userPointer.SetColor("_Color", Color.blue);
            yield return new WaitForSeconds(4f);
            userPointer.SetColor("_Color", color);
            yield return new WaitForSeconds(10f);
        }
    }
}
