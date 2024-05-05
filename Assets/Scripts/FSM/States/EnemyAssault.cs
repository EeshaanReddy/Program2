using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Testagent;
[CreateAssetMenu(menuName = "FSM/States/Attack")]
public class EnemyAssault : StatesBaseClass
{
    //public GameObject Behan;
    private FSMTransitions transition;
    public StatesBaseClass fleestate;
    private bool started = false;
    // Action
    public ActionBase PunchAction;
    private float wait = 3;

    public override void OnEnter(FSMTransitions enemy)
    {
        started = false;
        _animator = enemy.GetComponent<Animator>();
        agent = enemy.GetComponent<TestAgent>();
        _animator.SetBool("Walk", true);
        agent.MoveTo(new Vector3(-11.596f, -0.006f, -35.023f));
    }
    public override void OnExit(FSMTransitions enemy) 
    {
        PunchAction.OffAnimation(enemy.gameObject);

    }
    public override void UpdateState(FSMTransitions enemy) 
    {
        if (wait > 0)
        {
            wait -= Time.deltaTime;
            return;
        }
        if (atTarget())
        {
            if (!started)
            {
                started = true;
                agent.StartCoroutine(waitToBreak(enemy));
                PunchAction.OnAnimation(enemy.gameObject);
            }
        }
    }

    private IEnumerator waitToBreak(FSMTransitions enemy)
    {
        yield return new WaitForSeconds(1);
        transition = enemy.GetComponent<FSMTransitions>();
        transition.TransitionToState(transition.EnemyJailbreak);
    }
}