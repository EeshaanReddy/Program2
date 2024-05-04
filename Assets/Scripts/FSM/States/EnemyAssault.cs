using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Testagent;
[CreateAssetMenu(menuName = "FSM/States/Attack")]
public class EnemyAssault : StatesBaseClass
{
    private TestAgent agent;
    //public GameObject Behan;
    private FSMTransitions transition;
    public StatesBaseClass fleestate;
    // Action
    public ActionBase PunchAction;

    public override void OnEnter(FSMTransitions enemy)
    {
        agent = enemy.GetComponent<TestAgent>();
        agent.MoveTo(new Vector3(-3.5f, -0.02f, -36.9f));
    }
    public override void OnExit(FSMTransitions enemy) 
    {
        PunchAction.OffAnimation(enemy.gameObject);

    }
    public override void UpdateState(FSMTransitions enemy) 
    {

        if (Vector3.Distance(enemy.transform.position, new Vector3(-3.5f, -0.02f, -36.9f)) < 1f)
        {
            PunchAction.OnAnimation(enemy.gameObject);
        }
    }
}
