using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GunRecoilManager : MonoBehaviour
{
    [SerializeField] private Shootable shootable;
    [SerializeField] private float pushBackAmount = 0.2f;
    [SerializeField] private float maxPushedBackDist = 1.0f;
    [SerializeField] private float pushUpAmount;
    [SerializeField] private float epAmount = 1.2f;

    private Vector3 pushBackVector = new Vector3(0, 0, 0);
    private Vector3 idlePos;


    private float ep = 0; // 무질서도

    private void Start()
    {

        idlePos = transform.localPosition;

        pushBackVector.z = pushBackAmount;

        pushBackVector = transform.localPosition - pushBackVector;

        shootable.OnFire += () => {
            ep = 1.0f;
        };
    }


    private void Update()
    {
        // 무질서도 연산
        ep = Mathf.Clamp01(ep -= Time.deltaTime * epAmount);

        transform.localPosition = Vector3.Lerp(idlePos, pushBackVector, ep);

    }
}
