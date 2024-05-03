using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace Testagent
{
    public class TestAgent : MonoBehaviour
    {

        // Agent AI
        NavMeshAgent agent;
        // animation controller
        Animator animController;
        // the player's character
        GameObject player;
        // Sheriff Behan
        GameObject Behan;

        // Start is called before the first frame update
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();

            animController = GetComponent<Animator>();

            player = GameObject.FindGameObjectWithTag("Player");
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                agent.SetDestination(player.transform.position);
                animController.SetBool("Walk", true);
            }

            if (HasArrived() && !agent.isStopped)
            {
                animController.SetBool("Walk", false);
            }
        }

        // has the agent arrived at its target?
        bool HasArrived()
        {
            //Debug.Log("Arrived at target" + target);
            Vector3 direction = agent.destination - transform.position;

            return (direction.magnitude < agent.stoppingDistance);
        }

        public void MoveTo(Vector3 position)
        {
            agent.SetDestination(position);
            animController.SetBool("Walk", true);
        }

        public void SetAnimation(string animationName, bool state)
        {
            Debug.Log($"Attempting to set animation '{animationName}' to {state}.");
            if (animController == null)
            {
                Debug.LogError("Animator controller is null.");
                return;
            }
            animController.SetBool(animationName, state);
            Debug.Log($"Animation '{animationName}' set to {state}.");
        }
    }
}
