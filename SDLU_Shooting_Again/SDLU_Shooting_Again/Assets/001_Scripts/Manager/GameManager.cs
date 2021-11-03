using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoSingleton<GameManager>
{
    public CharactorBase Player { get; private set; }
    private void Awake()
    {
        Player = GameObject.Find("Player").GetComponent<CharactorBase>();
    }
    
}
