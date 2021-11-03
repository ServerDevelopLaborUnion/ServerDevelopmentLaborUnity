using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GunRecoilManager : MonoBehaviour
{
    [SerializeField] private Shootable shootable;

    // 총 푸쉬백
    [Header("Rand + pushBackAmount")]
    [SerializeField] private float pushBackAmount = 0.2f;
    [Range(0.0f, 1.5f)]
    [SerializeField] private float pushBackRandAmount = 0.2f;
    
    // 총 반동
    [Header("Rand + pushUpAmount")]
    [SerializeField] private float pushUpAmount = 0.2f;
    [Range(0.0f, 3.0f)]
    [SerializeField] private float pushUpRandAmuont = 1.0f;


    // 플레이어 반동
    [Header("Rand + playerRecoil")]
    [SerializeField] private float playerRecoil = 0.5f;
    [Range(-2.0f, 0.0f)]
    [SerializeField] private float playerRecoilRand = 0.5f;


    // 무질서도 연산 부스트
    [SerializeField] private float epAmount = 1.2f;

    private Vector3 pushBackVector = new Vector3(0, 0, 0);
    private Vector3 pushUpVector = new Vector3(0, 0, 0);
    private Vector3 playerRecoilVector = new Vector3(0, 0, 0);

    private Vector3 idlePos;
    private Vector3 idleRot;


    private float ep = 0; // 무질서도

    private void Start()
    {
        // 기본 Transform
        idlePos = transform.localPosition;
        idleRot = transform.localEulerAngles;


        shootable.OnFire += () => {
            ep = 1.0f;

            // 총 푸쉬백
            pushBackVector.z = Random.Range(pushBackAmount, pushBackAmount + pushBackRandAmount);
            pushBackVector = idlePos - pushBackVector;

            // 총 반동
            pushUpVector.x = Random.Range(pushUpAmount, pushUpAmount + pushUpRandAmuont);

            // 실제 반동
            playerRecoilVector = -transform.right;
            playerRecoilVector.x -= Random.Range(playerRecoil, playerRecoil + playerRecoilRand);
            // GameManager.Instance.Player.transform.eulerAngles += playerRecoilVector;
            GameManager.Instance.Player.transform.eulerAngles += playerRecoilVector;
        };
    }


    private void Update()
    {
        // 무질서도 연산
        ep = Mathf.Clamp01(ep -= Time.deltaTime * epAmount);

        transform.localPosition    = Vector3.Lerp(idlePos, pushBackVector, ep);
        transform.localEulerAngles = Vector3.Lerp(idleRot, pushUpVector, ep);
    }
}
