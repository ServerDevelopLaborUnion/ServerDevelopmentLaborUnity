using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapManager : MonoBehaviour
{
    [SerializeField]
    private Material otherPlayerPointer = null;

    [SerializeField]
    private RectTransform  minimap = null;

    private RectTransform minimapMask = null;
    private RectTransform minimapImage = null;

    void Start()
    {
        minimapMask = minimap.GetChild(0).GetComponent<RectTransform>();
        minimapImage = minimapMask.GetChild(0).GetComponent<RectTransform>();
        minimapImage.SetParent(minimapMask, false);
        StartCoroutine(ShowPosition());
    }

    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
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
