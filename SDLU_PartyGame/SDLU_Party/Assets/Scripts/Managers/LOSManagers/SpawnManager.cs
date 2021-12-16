using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefabs;

    void Start()
    {
        SpawnAI();
    }

    
    void Update()
    {
        
    }

    private void SpawnAI()
    {
        for(int i = 0; i < 50;i++){
            Instantiate(_enemyPrefabs, new Vector3(Random.Range(-48,48),2,Random.Range(-48,48)), Quaternion.identity);
        }
    }
}
