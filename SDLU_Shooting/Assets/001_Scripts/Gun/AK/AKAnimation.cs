using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AKAnimation : MonoBehaviour
{
    [SerializeField] private Animator anim = null; // 에니메이션
    private FullAutoGuns gun = null; // 총 상태
    private CharactorInput input = null; // 키 입력

    // 이렇게 하면 조금 더 빨라집니다.
    private readonly int fireHash   = Animator.StringToHash("Fire"); 
    private readonly int reloadHash = Animator.StringToHash("Reload"); 
    private readonly int aimHash    = Animator.StringToHash("Aim");

    private void Awake()
    {
        gun = GetComponent<FullAutoGuns>();
    }

    private void Start()
    {    
        input = GameManager.instance.player.GetComponent<CharactorInput>();
    }

    private void Update()
    {
        anim.SetBool(reloadHash, input.Reload); // 재장전 에니메이션이 두번 재생되는 문제가 있었습니다.
        anim.SetBool(fireHash, input.Shoot && !gun.MagEmpty); // 누르는 동안 재생함, 총알이 안 남아 있으면 재생 안함
        anim.SetBool(aimHash, input.Aim); // 누르는 동안 줌
    }
}
