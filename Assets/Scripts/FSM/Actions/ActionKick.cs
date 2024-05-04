using System.Collections;
using System.Collections.Generic;
using Testagent;
using UnityEngine;

// this is a test script to check for action scriptables

[CreateAssetMenu(menuName = "FSM/Actions/Kick")]
public class ActionKick : ActionBase
{
    private TestAgent agent;
    private FSMTransitions transotion;

    public override void OnAnimation(GameObject actor)
    {
        agent = actor.GetComponent<TestAgent>();
        agent.SetAnimation("Kick", true);

    }

    public override void OffAnimation(GameObject actor)
    {
        agent = actor.GetComponent<TestAgent>();
        agent.SetAnimation("Kick", false);

    }
}