﻿using System.Collections;
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

    public int DrillState;
    public int SentryState;

    public const int PATROL = 0;
    public const int FOLLOW = 1;
    public const int ATTACK = 2;
    public const int DISABLED = 3;

    private GameObject[] attackers;
    private bool summoned = false;

    private GameObject closest = null;

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        DrillState = PATROL;
        SentryState = PATROL;
    }

    // Update is called once per frame
    void Update() {

        if (gameObject.CompareTag("AttackEnemy"))
        {
            switch (DrillState)
            {
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
                    if (Vector3.Distance(agent.transform.position, agent.destination) < agent.stoppingDistance)
                    {
                        DrillState = ATTACK;
                        agent.isStopped = true;
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
        if(gameObject.CompareTag("SentryEnemy"))
        {
            switch (SentryState)
            {
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
                    if (Vector3.Distance(agent.transform.position, agent.destination) < agent.stoppingDistance)
                    {
                        agent.isStopped = true;
                    }
                    break;
                case DISABLED:
                    agent.isStopped = true;
                    break;
                default:
                    break;
            }
        }
	}

    public void OnDetect()
    {
        if (gameObject.CompareTag("AttackEnemy"))
        {
            agent.speed = 7;
            DrillState = FOLLOW;
        }
        if (gameObject.CompareTag("SentryEnemy"))
        {
            SentryState = FOLLOW;
            agent.speed = 10;
            if(!summoned)
            {
                attackers = GameObject.FindGameObjectsWithTag("AttackEnemy");
                float minDistance = Mathf.Infinity;

                foreach(GameObject go in attackers)
                {
                    float dist = Vector3.Distance(go.transform.position, player.transform.position);
                    if( dist < minDistance)
                    {
                        minDistance = dist;
                        closest = go;
                    }
                }
                if(closest != null)
                    closest.gameObject.GetComponent<AIMovement>().OnDetect();
                summoned = true;
            }
        }
    }

    public void Patrol()
    {
        agent.isStopped = false;
        if (gameObject.CompareTag("AttackEnemy"))
        {
            agent.speed = 3.5f;
            DrillState = PATROL;
        }
        if (gameObject.CompareTag("SentryEnemy"))
        {
            SentryState = PATROL;
            agent.speed = 5;
            if (summoned)
            {
                closest.gameObject.GetComponent<AIMovement>().Patrol();
                summoned = false;
            }
        }
    }

    public void OnEMP()
    {
        DrillState = DISABLED;
        SentryState = DISABLED;
    }
}