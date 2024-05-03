using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatesBaseClass : ScriptableObject
{
    // this the base class for all States
    public abstract void OnEnter(FSMTransitions enemy);
    public abstract void OnExit(FSMTransitions enemy);
    public abstract void UpdateState(FSMTransitions enemy);
}
