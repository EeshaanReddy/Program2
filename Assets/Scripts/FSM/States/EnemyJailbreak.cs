using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Testagent;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "FSM/States/Jailbreak")]
public class EnemyJailbreak : StatesBaseClass
{
    private FSMTransitions transition;
    private TestAgent agent;
    private List<Progress> BTreeAttempts = new List<Progress>( );
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
        Debug.Log("Entering Jailbreak State");
        for(int i =0; i<4; i++)
            BTreeAttempts.Add(Progress.unattempted);
        agent = enemy.GetComponent<TestAgent>();
        if (Random.value < 0.25f) unlocked = true;
    }

    public override void OnExit(FSMTransitions enemy)
    {
        Debug.Log("Exiting Jailbreak State");
    }

    public override void UpdateState(FSMTransitions enemy)
    {
        if (BTreeAttempts[0] == Progress.unattempted)
            AttemptToOpenCell();
        else if (BTreeAttempts[0] == Progress.inprogress)
            return;
        else if (BTreeAttempts[0] == Progress.success)
        {
            if (BTreeAttempts[4] == Progress.unattempted)
            {
                LetBillyOut(enemy);
            }
        }
        else if(BTreeAttempts[1] == Progress.unattempted)
            UseKeyToOpenCell();
        else if (BTreeAttempts[1] == Progress.inprogress)
            return;
        else if (BTreeAttempts[1] == Progress.success)
        {
            if (BTreeAttempts[4] == Progress.unattempted)
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
            if (BTreeAttempts[4] == Progress.unattempted)
            {
                LetBillyOut(enemy);
            }
        }
    }

    private IEnumerator Open(int index)
    {
        BTreeAttempts[index] = Progress.inprogress;
        agent.MoveTo(new Vector3(-9.979f,0f,-37.373f)); // move to cell door
        while (true)
        {
            float dist=agent.agent.remainingDistance;
            if (!float.IsInfinity(dist) && agent.agent.pathStatus == NavMeshPathStatus.PathComplete &&
                agent.agent.remainingDistance == 0) break;
            yield return new WaitForSeconds(0.05f);
        }

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
            door.transform.Rotate(0,scalar*0.025f,0);
            amt -= scalar * Time.deltaTime;
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
        while (true)
        {
            float dist=agent.agent.remainingDistance;
            if (!float.IsInfinity(dist) && agent.agent.pathStatus == NavMeshPathStatus.PathComplete &&
                agent.agent.remainingDistance == 0) break;
            yield return new WaitForSeconds(0.05f);
        }
        agent.GetComponent<Animator>().SetBool("PickUp", true);
        yield return new WaitForSeconds(2);
        Destroy(GameObject.Find("Rusty_Key_01"));
        if (Random.value < 0.5)
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
        agent.MoveTo(new Vector3(11.924f, 0, -28.256f));
        while (true)
        {
            float dist=agent.agent.remainingDistance;
            if (!float.IsInfinity(dist) && agent.agent.pathStatus == NavMeshPathStatus.PathComplete &&
                agent.agent.remainingDistance == 0) break;
            yield return new WaitForSeconds(0.05f);
        }
        agent.GetComponent<Animator>().SetBool("PickUp", true);
        yield return new WaitForSeconds(2);
        GameObject.Find("Dynamite_Stick_01").SetActive(false);
        agent.MoveTo(new Vector3(-9.979f,0f,-37.373f)); // move to cell door
        while (true)
        {
            float dist=agent.agent.remainingDistance;
            if (!float.IsInfinity(dist) && agent.agent.pathStatus == NavMeshPathStatus.PathComplete &&
                agent.agent.remainingDistance == 0) break;
            yield return new WaitForSeconds(0.05f);
        }
        agent.GetComponent<Animator>().SetBool("PickUp", true);
        yield return new WaitForSeconds(2);
        GameObject.Find("Dynamite_Stick_01").SetActive(true);
        GameObject.Find("Dynamite_Stick_01").transform.position = new Vector3(-9.292f, 0, -37.764f);
        agent.MoveTo(new Vector3(11.924f, 0, -26.256f));
        yield return new WaitForSeconds(3);
        Destroy(GameObject.Find("Dynamite_Stick_01"));
        Destroy(GameObject.Find("JailDoor"));
        Debug.Log("Boom");
        BTreeAttempts[3] = Progress.success;
    }

    private void LetBillyOut(FSMTransitions enemy)
    {
        //stub, here in case we need it for something
    }
}
