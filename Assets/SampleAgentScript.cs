using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SampleAgentScript : MonoBehaviour {
    public Transform target1;
    public Transform target2;
    NavMeshAgent agent;
    private bool tar = true;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        if (tar)
            agent.SetDestination(target1.position);
        else
            agent.SetDestination(target2.position);

        if(Vector3.Distance(agent.transform.position, agent.destination) < agent.stoppingDistance)
        {
            tar = !tar;
        }
	}
}
