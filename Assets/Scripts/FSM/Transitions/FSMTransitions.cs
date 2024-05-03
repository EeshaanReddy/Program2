using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMTransitions : MonoBehaviour
{
    public StatesBaseClass currentState;
    public StatesBaseClass EnemyIdle;
    public StatesBaseClass EnemyPatrol;
    public StatesBaseClass EnemyAssault;
    public StatesBaseClass EnemyFlee;

    void Start()
    {
        // this will be different for Ike and Belle, yet to add a condition
        TransitionToState(currentState);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void TransitionToState(StatesBaseClass newState)
    {
        currentState?.OnExit(this);
        currentState = newState;
        currentState.OnEnter(this);
    }
}
