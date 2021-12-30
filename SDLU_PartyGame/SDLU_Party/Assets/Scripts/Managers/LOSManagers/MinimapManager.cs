using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapManager : MonoBehaviour
{
    [SerializeField]
    private Material otherPlayerPointer = null;

    [SerializeField]
    private GameObject  minimap = null;

    [SerializeField]
    private Camera minimapCamera = null;

    [SerializeField]
    private Sprite circle = null;

    [SerializeField]
    private Sprite square = null;

    private GameObject minimapMask = null;
    private Transform minimapImage = null;

    private Transform minimapImageTransformMemory = null;

    void Start()
    {
        minimapMask = minimap.transform.GetChild(0).gameObject;
        minimapImage = minimapMask.transform.GetChild(0).GetComponent<Transform>();
        //minimapImage.SetParent(minimapMask, false);
        minimapImageTransformMemory = minimapImage.transform;
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
        var minimapMaskRectTransform = minimapMask.GetComponent<RectTransform>();
        yield return new WaitForSeconds(4f);
        while(true){
            minimapMask.GetComponent<Image>().sprite = square;
            minimapMaskRectTransform.sizeDelta = new Vector2(350, 350);
            otherPlayerPointer.SetColor("_Color", Color.red);
            yield return new WaitForSeconds(4f);
            minimapMask.GetComponent<Image>().sprite = circle;
            minimapMaskRectTransform.sizeDelta = new Vector2(150, 150);
            otherPlayerPointer.SetColor("_Color", oColor);
            yield return new WaitForSeconds(10f);
        }
    }
}
