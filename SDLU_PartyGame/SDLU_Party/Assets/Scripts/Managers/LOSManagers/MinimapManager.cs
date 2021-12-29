using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapManager : MonoBehaviour
{
    [SerializeField]
    private Material otherPlayerPointer = null;

    [SerializeField]
    private RectTransform  minimap = null;

    [SerializeField]
    private Camera minimapCamera = null;

    private RectTransform minimapMask = null;
    private Transform minimapImage = null;

    private Transform minimapImageTransformMemory = null;

    void Start()
    {
        minimapMask = minimap.GetChild(0).GetComponent<RectTransform>();
        minimapImage = minimapMask.GetChild(0).GetComponent<Transform>();
        minimapImage.SetParent(minimapMask, false);
        minimapImageTransformMemory = minimapImage.transform;
        StartCoroutine(ShowPosition());
    }

    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        Vector3 playerPosition = FindObjectOfType<LOSPlayer>().transform.position;
        Vector3 playerPositionInMinimap = minimapCamera.WorldToViewportPoint(playerPosition);
        minimapMask.anchoredPosition = new Vector2(playerPositionInMinimap.x * minimapMask.sizeDelta.x, playerPositionInMinimap.y * minimapMask.sizeDelta.y);
        minimapImage.transform.position = minimapImageTransformMemory.position;
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
