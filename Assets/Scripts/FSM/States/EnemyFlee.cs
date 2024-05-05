using System.Collections;
using System.Collections.Generic;
using Testagent;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/States/Flee")]
public class EnemyFlee : StatesBaseClass
{
    private FSMTransitions transition;
    Vector3 runLoc = new Vector3(64f, -0.5f, 6.1f);

    public override void OnEnter(FSMTransitions enemy)
    {
        agent = enemy.GetComponent<TestAgent>();
        agent.MoveTo(runLoc);
    }
    public override void OnExit(FSMTransitions enemy) { }
    public override void UpdateState(FSMTransitions enemy) 
    {
        if (Vector3.Distance(enemy.transform.position, runLoc) < 1f)
        {
            transition.TransitionToState(transition.EnemyIdle);
        }
    }
}   