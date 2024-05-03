using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Testagent;

[CreateAssetMenu(menuName = "FSM/States/Jailbreak")]
public class EnemyJailbreak : StatesBaseClass
{
    private FSMTransitions transition;
    private TestAgent agent;

    public override void OnEnter(FSMTransitions enemy)
    {
        Debug.Log("Entering Jailbreak State");
        PerformJailbreak(enemy);
    }

    public override void OnExit(FSMTransitions enemy)
    {
        Debug.Log("Exiting Jailbreak State");
    }

    public override void UpdateState(FSMTransitions enemy)
    {
        // Update can monitor completion of jailbreak, or handle retries/failures
    }

    private void PerformJailbreak(FSMTransitions enemy)
    {
        agent = enemy.GetComponent<TestAgent>();

        if (AttemptToOpenCell())
            return;

        if (UseKeyToOpenCell())
            return;

        if (BlastOpenCellDoor(enemy))
            return;

        LetBillyOut(enemy);
    }

    private bool AttemptToOpenCell()
    {
        
        agent.MoveTo(new Vector3(0f,0f,0f)); // move to cell door

        if (Random.value < 0.25f) 
        {
            Debug.Log("Cell door was unlocked!");
            return true;
        }
        return false;
    }

    private bool UseKeyToOpenCell()
    {
        agent.MoveTo(new Vector3(0f, 0f, 0f)); // move to key
        // pick up key
        agent.MoveTo(new Vector3(0f, 0f, 0f)); // move back to cell

        if (Random.value < 0.50f)  
        {
            Debug.Log("Used rusty key to open the cell!");
            return true;
        }
        return false;
    }

    private bool BlastOpenCellDoor(FSMTransitions enemy)
    {
        // Assume dynamite is acquired
        Debug.Log("Placing dynamite to blast open the cell door.");
        // Simulate delay for placing and lighting dynamite
        enemy.StartCoroutine(BlastDelay(enemy));
        return true;
    }

    private IEnumerator BlastDelay(FSMTransitions enemy)
    {
        yield return new WaitForSeconds(5);  // Simulate time for dynamite to explode
        Debug.Log("Dynamite exploded and opened the cell door!");
        LetBillyOut(enemy);
    }

    private void LetBillyOut(FSMTransitions enemy)
    {
        Debug.Log("Billy is now free, exit the cell.");
        
    }
}
