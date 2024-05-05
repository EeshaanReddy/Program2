using System.Collections;
using System.Collections.Generic;
using Testagent;
using UnityEngine;
using UnityEngine.AI;

public abstract class StatesBaseClass : ScriptableObject
{
    protected TestAgent agent;

    protected Animator _animator;
    
    // this the base class for all States
    public abstract void OnEnter(FSMTransitions enemy);
    public abstract void OnExit(FSMTransitions enemy);
    public abstract void UpdateState(FSMTransitions enemy);
    
    protected bool atTarget()
    {
        float dist=agent.agent.remainingDistance;
        if (!float.IsInfinity(dist) &&
            agent.agent.remainingDistance < 0.3f) return true;
        return false;
    }
}