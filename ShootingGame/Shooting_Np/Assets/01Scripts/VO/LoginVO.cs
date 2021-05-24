using UnityEngine;

public class LoginVO
{
    public string type;
    public string name;
    public LoginVO(){
        
    }
    public LoginVO(string type, string name){
        this.type = type;
        this.name = name;
    }
}
