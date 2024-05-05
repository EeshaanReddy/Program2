using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Testagent;

[CreateAssetMenu(menuName = "FSM/States/Patrol")]
public class EnemyPatrol : StatesBaseClass
{
    private FSMTransitions transition;
    public StatesBaseClass fleestate;
    // Action
    public ActionBase KickAction;

    public override void OnEnter(FSMTransitions enemy) 
    {
        agent = enemy.GetComponent<TestAgent>();
        agent.MoveTo(new Vector3(-3.5f, -0.02f, -36.9f));
    }
    public override void OnExit(FSMTransitions enemy) 
    {
        // KickAction.OnAnimation(enemy.gameObject);
    }
    public override void UpdateState(FSMTransitions enemy) 
    {
        
        if (Vector3.Distance(enemy.transform.position, new Vector3(-3.5f, -0.02f, -36.9f)) < 1f)
        {
            // KickAction.OnAnimation(enemy.gameObject);
        }

    }
}