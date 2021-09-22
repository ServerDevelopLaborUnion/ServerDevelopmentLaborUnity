using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTransformManager : MonoBehaviour
{
    [SerializeField] private Transform aimTrm = null; // 조준 포지션
    [SerializeField] private Transform gunTrm = null; // 실제 총 포지션
    private Vector3 aimPos;
    private Vector3 defaultPos;

    [SerializeField] private float shakeAmount = 2.0f;
    [SerializeField] private float pushbackAmount = 0.2f;
    [SerializeField] private float recoilAmount = 0.5f;


    private float t = 1.0f; // 총을 기본 위치로 돌리기 위해

    private void Awake()
    {
        defaultPos = transform.localPosition;
        aimPos = aimTrm.localPosition;

        StartCoroutine(GunEntropy());
        // 왜 함수 이름이 GunEntropy 인 것인가 에 대한 주석
        /*
        Entropy: 무질서도

        자연에 존재하는 모든 물체는 무질서한 방향으로 흘러간다고 합니다.
        이를 무질서도가 증가한다고 함.

        자연 상태로 돌아가는 것이죠 흠흠
        쇠에 녹이 쓸고 부서지기 쉽게 되는 것 처럼

        이건 물리 이야기이니 만약 더 궁금하다면 정화쌤에게
        */
    }

    void Start()
    {
        Shootable.OnAim += (t1) => {
            transform.localPosition = Vector3.Lerp(defaultPos, aimPos, t1);
        };

        Shootable.OnFire += () => {
            gunTrm.localEulerAngles += Vector3.forward * Random.Range(-shakeAmount, shakeAmount); // 좌후 흔들림
            // gunTrm.localEulerAngles += Vector3.up * Random.Range(-shakeAmount, shakeAmount);
            gunTrm.localEulerAngles += Vector3.left * recoilAmount; // 반동 (위로 들리는)
            // gunTrm.localEulerAngles += new Vector3(-recoilAmount, Random.Range(-shakeAmount, shakeAmount), Random.Range(-shakeAmount, shakeAmount));

            gunTrm.localPosition    += Vector3.back * pushbackAmount; // 반동 (뒤로 밀리는)
            t = 0.0f;
        };
    }

    IEnumerator GunEntropy()
    {
        

        while(true)
        {
            gunTrm.localPosition    = Vector3.Lerp(gunTrm.localPosition,    Vector3.zero,        t);
            gunTrm.localRotation    = Quaternion.Lerp(gunTrm.localRotation, Quaternion.identity, t);

            t += Time.deltaTime;

            yield return null;
        }
    }
}