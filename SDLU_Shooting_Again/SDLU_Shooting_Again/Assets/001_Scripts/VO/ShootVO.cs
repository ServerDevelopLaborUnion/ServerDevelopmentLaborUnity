using UnityEngine;

[System.Serializable]
public class ShootVO 
{
    public int id;

    public Vector3 rotation;
    public Vector3 position;


    public ShootVO(int id, Vector3 position , Vector3 rotation){
        this.id = id;
        this.rotation = rotation;
        this.position = position;
    }
}
