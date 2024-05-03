using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Testagent;

[CreateAssetMenu(menuName = "FSM/States/Patrol")]
public class EnemyPatrol : StatesBaseClass
{
    private TestAgent agent;
    private FSMTransitions transition;
    public StatesBaseClass fleestate;

    public override void OnEnter(FSMTransitions enemy) 
    {
        agent = enemy.GetComponent<TestAgent>();
        agent.MoveTo(new Vector3(-12.5f, -0.02f, -33.9f));
    }
    public override void OnExit(FSMTransitions enemy) 
    {
        //agent = enemy.GetComponent<TestAgent>();
        //agent.SetAnimation("Kick", false);
    }
    public override void UpdateState(FSMTransitions enemy) 
    {
        
        if (Vector3.Distance(enemy.transform.position, new Vector3(-12.5f, -0.02f, -33.9f)) < 1f)
        {
            //agent = enemy.GetComponent<TestAgent>();
            //agent.SetAnimation("Kick", true);
        }

    }
}
