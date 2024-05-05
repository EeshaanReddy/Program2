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
        if (SenseManager.manager.CanSee(gameObject) && waitTime <= 0)
        {
            guard();
        }
        if (waitTime > 0)
        {
            waitTime -= Time.deltaTime;
        }
        if (waitTime <= 0 && GetComponent<Animator>().GetBool("Crouch"))
        {
            stop();
        }
    }

    public void guard()
    {
        GetComponent<FSMTransitions>().enabled = false;
        GetComponent<Animator>().SetBool("Crouch", true);
        GetComponent<NavMeshAgent>().isStopped = true;
        waitTime = 5; 
    }

    public void stop()
    {
        FSMTransitions banditFSM = GetComponent<FSMTransitions>();
        banditFSM.enabled = true;
        GetComponent<Animator>().SetBool("Crouch", false);
        GetComponent<NavMeshAgent>().isStopped = false;
        banditFSM.TransitionToState(banditFSM.currentState);
        
    }
}
