using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour {
    // Reads in 2 empty objects as target locations
    public Transform target1;
    public Transform target2;
    NavMeshAgent agent;
    public GameObject player;

    private bool tar = true; // Controls which target is the destination

    private int AIState;

    public const int PATROL = 0;
    public const int FOLLOW = 1;
    public const int ATTACK = 2;
    public const int DISABLED = 3;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        AIState = PATROL;
    }

    // Update is called once per frame
    void Update() {

        switch (AIState) {
            case PATROL:
                // Set the Nav Agent's destination based on the tar variable
                if (tar)
                    agent.SetDestination(target1.position);
                else
                    agent.SetDestination(target2.position);

                // Switch targets once the target is reached
                if (Vector3.Distance(agent.transform.position, agent.destination) < agent.stoppingDistance)
                {
                    tar = !tar;
                }
                break;
            case FOLLOW:
                agent.SetDestination(player.transform.position);
                if(Vector3.Distance(agent.transform.position, agent.destination) < agent.stoppingDistance)
                {
                    AIState = ATTACK;
                }
                break;
            case ATTACK:
                player.GetComponent<Die>().PlayerDie();
                break;
            case DISABLED:
                agent.isStopped = true;
                break;
            default:
                break;
        }
	}

    public void OnDetect()
    {
        AIState = FOLLOW;
    }

    public void Patrol()
    {
        AIState = PATROL;
    }
}
