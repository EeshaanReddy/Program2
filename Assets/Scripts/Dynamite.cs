using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//for some reason gameobject.find won't work on the dynamite. also had to put the dynamite outside
//because every time I try to rebake the navmesh so the bandit can get in the door everything breaks
public class Dynamite : MonoBehaviour
{
    public static GameObject dynamite;

    private void Awake()
    {
        dynamite = gameObject;
    }
}
