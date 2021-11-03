using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class HistoryVO
{
    public List<int> id = new List<int>();
    public List<string> pos = new List<string>();
    public List<int> hp = new List<int>();


    public HistoryVO(List<int> id, List<string> pos, List<int> hp)
    {
        this.id = id;
        this.pos = pos;
        this.hp = hp;
    }
    
}