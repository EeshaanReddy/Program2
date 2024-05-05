using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Testagent;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "FSM/States/Jailbreak")]
public class EnemyJailbreak : StatesBaseClass
{
    private FSMTransitions transition;
    private List<Progress> BTreeAttempts;
    private bool unlocked;

    private enum Progress
    {
        unattempted,
        inprogress,
        success,
        failure
    }

    public override void OnEnter(FSMTransitions enemy)
    {
        _animator = enemy.GetComponent<Animator>();
        BTreeAttempts = new List<Progress>();
        for (int i = 0; i < 4; i++)
            BTreeAttempts.Add(Progress.unattempted);
        agent = enemy.GetComponent<TestAgent>();
        unlocked = false;
        if (Random.Range(0f, 1f) < 0.25f) unlocked = true;
    }

    public override void OnExit(FSMTransitions enemy)
    {
    }

    public override void UpdateState(FSMTransitions enemy)
    {
        if (BTreeAttempts[0] == Progress.unattempted)
            AttemptToOpenCell();
        else if (BTreeAttempts[0] == Progress.inprogress)
            return;
        else if (BTreeAttempts[0] == Progress.success)
        {
            if (BTreeAttempts[3] == Progress.unattempted)
            {
                LetBillyOut(enemy);
            }
        }
        else if (BTreeAttempts[1] == Progress.unattempted)
            UseKeyToOpenCell();
        else if (BTreeAttempts[1] == Progress.inprogress)
            return;
        else if (BTreeAttempts[1] == Progress.success)
        {
            if (BTreeAttempts[3] == Progress.unattempted)
            {
                LetBillyOut(enemy);
            }
        }
        else if (BTreeAttempts[2] == Progress.unattempted)
            BlastOpenCellDoor(enemy);
        else if (BTreeAttempts[2] == Progress.inprogress)
            return;
        else if (BTreeAttempts[2] == Progress.success)
        {
            if (BTreeAttempts[3] == Progress.unattempted)
            {
                LetBillyOut(enemy);
            }
        }
    }

    private IEnumerator Open(int index)
    {
        BTreeAttempts[index] = Progress.inprogress;
        _animator.SetBool("Walk", true);
        agent.MoveTo(new Vector3(-9.979f, 0f, -37.373f)); // move to cell door
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(atTarget);
        _animator.SetBool("Walk", false);
        yield return new WaitForSeconds(0.5f);
        if (unlocked)
        {
            GameObject door = GameObject.Find("JailDoor");
            agent.StartCoroutine(OpenDoor(door));
            BTreeAttempts[index] = Progress.success;
        }
        else
        {
            BTreeAttempts[index] = Progress.failure;
        }
    }

    private IEnumerator OpenDoor(GameObject door)
    {
        float amt = 90;
        float scalar = 45;
        while (amt > 0)
        {
            door.transform.Rotate(0, scalar * 0.025f, 0);
            amt -= scalar * 0.025f;
            yield return new WaitForSeconds(0.025f);
        }
    }

    private void AttemptToOpenCell()
    {
        agent.StartCoroutine(Open(0));
    }

    private IEnumerator GetKey()
    {
        BTreeAttempts[1] = Progress.inprogress;
        agent.MoveTo(new Vector3(-10.779f, 0f, -34.32f)); // move to key
        _animator.SetBool("Walk", true);
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(atTarget);
        _animator.SetBool("Walk", false);
        _animator.SetBool("PickUp", true);
        yield return new WaitForSeconds(2);
        _animator.SetBool("PickUp", false);
        Destroy(GameObject.Find("Rusty_Key_01"));
        if (Random.Range(0f, 1f) < 0.5)
        {
            unlocked = true;
        }
        agent.StartCoroutine(Open(1));
    }

    private void UseKeyToOpenCell()
    {
        agent.StartCoroutine(GetKey());
    }

    private void BlastOpenCellDoor(FSMTransitions enemy)
    {
        agent.StartCoroutine(blow());
    }

    private IEnumerator blow()
    {
        BTreeAttempts[2] = Progress.inprogress;
        GameObject dynamite = Dynamite.dynamite;
        agent.MoveTo(new Vector3(3.857f, -0.006f, -27.813f));
        _animator.SetBool("Walk", true);
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(atTarget);
        _animator.SetBool("Walk", false);
        _animator.SetBool("PickUp", true);
        yield return new WaitForSeconds(2);
        dynamite.SetActive(false);
        _animator.SetBool("PickUp", false);
        _animator.SetBool("Walk", true);
        agent.MoveTo(new Vector3(-9.979f, 0f, -37.373f)); // move to cell door
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(atTarget);
        _animator.SetBool("Walk", false);
        _animator.SetBool("PickUp", true);
        yield return new WaitForSeconds(2);
        dynamite.SetActive(true);
        dynamite.transform.position = new Vector3(-9.292f, 0, -37.764f);
        _animator.SetBool("Walk", true);
        _animator.SetBool("PickUp", false);
        agent.MoveTo(new Vector3(-11.924f, 0, -36.256f));
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(atTarget);
        _animator.SetBool("Walk", false);
        yield return new WaitForSeconds(3);
        Destroy(GameObject.Find("Dynamite_Stick_01"));
        Destroy(GameObject.Find("JailDoor"));
        Debug.Log("Boom");
        BTreeAttempts[2] = Progress.success;
    }

    private void LetBillyOut(FSMTransitions enemy)
    {
        AllFlee.allFlee.flee();
    }
}
