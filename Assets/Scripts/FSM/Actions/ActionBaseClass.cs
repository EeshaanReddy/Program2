using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionBase : ScriptableObject
{
    // this is base class for actions
    public abstract void OnAnimation(GameObject actor);

    public abstract void OffAnimation(GameObject actor);
}