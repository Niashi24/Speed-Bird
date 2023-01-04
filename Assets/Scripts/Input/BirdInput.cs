using UnityEngine;
using System;

public abstract class BirdInput : MonoBehaviour
{
    public Action OnTap;
    public Action OnSwipeDown;
    public Action OnSwipeUp;

    public bool isHeld {get; protected set;}
}
