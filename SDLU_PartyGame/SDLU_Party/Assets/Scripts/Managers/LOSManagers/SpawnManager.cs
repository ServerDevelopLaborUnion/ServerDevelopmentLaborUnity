using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefabs;

    [SerializeField]
    private GameObject _otherPlayerPrefabs;

    void Start()
    {
        SpawnOtherPlayer();
        SpawnAI();
    }

    
    void Update()
    {
        
    }

    private void SpawnOtherPlayer(){
        for(int i = 0; i < 6;i++){
            Instantiate(_otherPlayerPrefabs, new Vector3(Random.Range(-48,48),2,Random.Range(-48,48)), Quaternion.identity);
        }
    }

    private void SpawnAI()
    {
        for(int i = 0; i < 50;i++){
            Instantiate(_enemyPrefabs, new Vector3(Random.Range(-48,48),2,Random.Range(-48,48)), Quaternion.identity);
        }
    }
}
