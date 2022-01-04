using System.Collections.Generic;
using UnityEngine;

public class UserManager : MonoSingleton<UserManager>
{
    public GameObject playerPrefab;

    private Dictionary<int, ChickenPlayer> userDictionary = new Dictionary<int, ChickenPlayer>(); // 유저를 가진 Dictionary

    public void AddUser(int id, Vector2? position = null, Quaternion? rotation = null)
    {
        if(userDictionary.ContainsKey(id)) {
            throw new System.ArgumentException($"Request id:{id} already added");
        }
        bool remote = SocketPlayer.Instance.ID == id;

        userDictionary.Add(id, Instantiate(playerPrefab, new Vector3(position.Value.x, playerPrefab.transform.position.y, position.Value.y), rotation.Value).GetComponent<ChickenPlayer>());
        
        userDictionary[id].isMe = remote;
        userDictionary[id].isPlaying = remote;
    }

    public void SetTransform(int id, Vector3 pos, Quaternion rot)
    {
        if(!userDictionary.ContainsKey(id)) {
            throw new System.ArgumentOutOfRangeException($"Request id:{id} not founded in dictionary");
        }

        userDictionary[id].transform.position = pos;
        userDictionary[id].transform.rotation = rot;
    }
}