using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SampleAgentScript : MonoBehaviour {
    // Reads in 2 empty objects as target locations
    public Transform target1;
    public Transform target2;
    NavMeshAgent agent;
    private bool tar = true; // Controls which target is the destination

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        // Set the Nav Agent's destination based on the tar variable
        if (tar)
            agent.SetDestination(target1.position);
        else
            agent.SetDestination(target2.position);

        // Switch targets once the target is reached
        if(Vector3.Distance(agent.transform.position, agent.destination) < agent.stoppingDistance)
        {
            tar = !tar;
        }
	}
}
