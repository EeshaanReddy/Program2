using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//basically cludging together a facsimile of a hierarchical state machine here because it'd take a lot longer to do it properly
public class GuardManager : MonoBehaviour
{
    private float waitTime = 0;
    private void Update()
    {
        if (SenseManager.manager.CanSee(gameObject))
        {
            guard();
        }

        waitTime -= Time.deltaTime;
        if(waitTime <= 0)
            stop();
    }

    public void guard()
    {
        GetComponent<FSMTransitions>().enabled = false;
        GetComponent<Animator>().SetBool("Crouch", true);
        GetComponent<NavMeshAgent>().isStopped = true;
        waitTime = 3;
    }

    public void stop()
    {
        FSMTransitions banditFSM = GetComponent<FSMTransitions>();
        banditFSM.enabled = true;
        GetComponent<NavMeshAgent>().isStopped = false;
        banditFSM.TransitionToState(banditFSM.currentState);
        GetComponent<Animator>().SetBool("Crouch", false);
    }
}
