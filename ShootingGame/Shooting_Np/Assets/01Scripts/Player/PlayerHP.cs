using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private PlayerRPC player;
    [SerializeField] private Slider hpBar;
    

    // Start is called before the first frame update
    void Start()
    {
        hpBar.maxValue = player.hp;
        hpBar.value = player.hp;
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.value = player.hp;
    }
}
