using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    public delegate void SingleInt(int val);
    public delegate void SingleVecter2(Vector2 val);
    public delegate void SingleBool(bool val);
    public delegate void ListInt(List<int> val);
}
