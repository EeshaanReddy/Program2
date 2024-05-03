using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Testagent;
[CreateAssetMenu(menuName = "FSM/States/Attack")]
public class EnemyAssault : StatesBaseClass
{
    private TestAgent agent;
    public GameObject Behan;
    private FSMTransitions transition;
    public StatesBaseClass fleestate;

    public override void OnEnter(FSMTransitions enemy)
    {
        agent = enemy.GetComponent<TestAgent>();
        agent.MoveTo(Behan.transform.position);
    }
    public override void OnExit(FSMTransitions enemy) 
    {
        agent = enemy.GetComponent<TestAgent>();
        agent.SetAnimation("Punch", false);
    
    }
    public override void UpdateState(FSMTransitions enemy) 
    {

        if (Vector3.Distance(enemy.transform.position, Behan.transform.position) < 1f)
        {
            transition = enemy.GetComponent<FSMTransitions>();
            agent.SetAnimation("Punch", true);
            transition.TransitionToState(transition.EnemyIdle);
        }
    }
}
