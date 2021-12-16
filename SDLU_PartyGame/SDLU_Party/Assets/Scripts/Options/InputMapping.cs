using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InputMapping : JsonObject
{
    public Mouse move = Mouse.RIGHT;
    public Mouse attack = Mouse.LEFT;
}
